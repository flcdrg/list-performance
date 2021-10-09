using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace ListPerformance
{
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class ListvSorted
    {
        private readonly List<string> _list;
        private readonly List<Stuff> _common;
        private readonly SortedSet<string> _sortedSet;
        private readonly HashSet<string> _hashSet;

        public ListvSorted()
        {
            _list = new List<string>();

            // load up 
            var r = new Random();
            for (int i = 0; i < 20000; i++)
            {
                _list.Add(r.Next(1000, 100000).ToString());
            }

            _common = _list.Select(x => new Stuff() { Key = x, Other = DateTime.Now.ToString(), Things = new List<string>(1000) }).ToList();

            _sortedSet = new SortedSet<string>(_list);

            _hashSet = new HashSet<string>(_list);
        }

        [Benchmark]
        public void AList()
        {
            _ = _common.Where(x => _list.Contains(x.Key)).ToList();
        }

        [Benchmark]
        public void ASortedSet()
        {
            _ = _common.Where(x => _sortedSet.Contains(x.Key)).ToList();
        }

        [Benchmark]
        public void AHashSet()
        {
            _ = _common.Where(x => _hashSet.Contains(x.Key)).ToList();
        }
    }
}