using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Excogitated.Common.Test
{
    [TestClass]
    public class RngTests
    {
        private const int _totalIterations = 10000;
        private const double _delta = 0.15;
        private const int _minRange = 1;
        private const int _maxRange = 10;

        [TestMethod]
        public void Uniformity_Int32_TrueRng()
        {
            var buckets = new Dictionary<int, AtomicInt32>();
            for (var i = _minRange; i <= _maxRange; i++)
                buckets[i] = new AtomicInt32();
            for (var i = 0; i < _totalIterations; i++)
                buckets[Rng.True.GetInt32(_minRange, _maxRange)].Increment();
            var expectedIterations = _totalIterations / _maxRange;
            var maxDelta = expectedIterations * _delta;
            for (var i = _minRange; i <= _maxRange; i++)
            {
                Console.WriteLine($"{i} : {buckets[i]}");
                Assert.AreEqual(expectedIterations, buckets[i], maxDelta, $"Bucket: {i}");
            }
        }

        [TestMethod]
        public void Uniformity_Int32_PseudoRng()
        {
            var buckets = new Dictionary<int, AtomicInt32>();
            for (var i = _minRange; i <= _maxRange; i++)
                buckets[i] = new AtomicInt32();
            for (var i = 0; i < _totalIterations; i++)
                buckets[Rng.Pseudo.GetInt32(_minRange, _maxRange)].Increment();
            var expectedIterations = _totalIterations / _maxRange;
            var maxDelta = expectedIterations * _delta;
            for (var i = _minRange; i <= _maxRange; i++)
            {
                Console.WriteLine($"{i} : {buckets[i]}");
                Assert.AreEqual(expectedIterations, buckets[i], maxDelta, $"Bucket: {i}");
            }
        }
    }
}
