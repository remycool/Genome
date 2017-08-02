using Cluster.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class MapReduce<TInput,TResult>
    {
        public static ConcurrentBag<TResult> resultBag = new ConcurrentBag<TResult>();
        public BlockingCollection<TResult> mapingInputs = new BlockingCollection<TResult>(resultBag);

        


        private void map(IEnumerable<TInput> inputs, Func<TInput, TResult> mapDelegate)
        {
          
            Parallel.ForEach(inputs, i =>
            {   
                mapingInputs.Add(mapDelegate(i));
            });
            mapingInputs.CompleteAdding();
        }

        public ConcurrentDictionary<int, TResult> resultStore = new ConcurrentDictionary<int, TResult>();

        
        private void reduce()
        {
            int i = 1;
            Parallel.ForEach(mapingInputs.GetConsumingEnumerable(), input =>
            {   
                resultStore.TryAdd(i++,input);
            });
        }

        public void mapReduce(IEnumerable<TInput> inputs, Func<TInput, TResult> function)
        {  
            if (mapingInputs.IsAddingCompleted)
            {
                resultBag = new ConcurrentBag<TResult>();
                mapingInputs = new BlockingCollection<TResult>(resultBag);
            }
         
            ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                map(inputs, function);
            });

            reduce();
        }
    }
}
