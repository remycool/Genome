using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cluster.Classes
{
    public class MapReduce
    {
        public Task<TResult> MapRed<TInput, TIntermediate, TResult>(
    IEnumerable<TInput> inputs,
    Func<TInput, TIntermediate> mapDelegate,
    Func<IEnumerable<TIntermediate>, Task<TResult>> reduceDelegate
)
        {
            ConcurrentBag<TIntermediate> results = new ConcurrentBag<TIntermediate>();
            inputs
                .AsParallel()
                .Select(mapDelegate)
                .ForAll(r => results.Add(r));
            return reduceDelegate(results);
        }

    }
}
