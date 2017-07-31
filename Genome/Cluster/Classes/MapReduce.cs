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

        public void map(IEnumerable<TInput> inputs, Func<TInput, TResult> mapDelegate)
        {
            Parallel.ForEach(inputs, i =>
            {   
                mapingInputs.Add(mapDelegate(i));
            });

            mapingInputs.CompleteAdding();
        }

        public ConcurrentDictionary<TResult, int> resultStore = new ConcurrentDictionary<TResult, int>();

        public void reduce()
        {
            Parallel.ForEach(mapingInputs.GetConsumingEnumerable(), input =>
            {   
                resultStore.AddOrUpdate(input, 1, (key, oldValue) => Interlocked.Increment(ref oldValue));
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
