using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace TransformProvider.Reflection.Tests
{
    internal class TestPredicateSet : PredicateSet
    {
        public TestPredicateSet(IEnumerable<KeyValuePair<string, bool>> predicates) : base(predicates)
        {
        }
    }

    [TestClass]
    public class PredicateSetTests
    {
        [TestMethod]
        public void PredicateSet_Constructor_ShouldSucceedWhenSequenceIsNull()
        {
            Func<TestPredicateSet> constructor = () => new TestPredicateSet(null);

            var predicateSet = constructor.Should().NotThrow().Subject;
            predicateSet.Should().NotBeNull();
            predicateSet.Predicates.Should().BeEmpty();
        }

        [TestMethod]
        public void PredicateSet_Constructor_ShouldSucceedWhenSequenceIsEmpty()
        {
            List<KeyValuePair<string, bool>> sequence = new List<KeyValuePair<string, bool>>();

            Func<TestPredicateSet> constructor = () => new TestPredicateSet(sequence);

            var predicateSet = constructor.Should().NotThrow().Subject;
            predicateSet.Should().NotBeNull();
            predicateSet.Predicates.Should().BeEmpty();
        }

        [TestMethod]
        public void PredicateSet_Constructor_ShouldSucceedWhenSequenceIsNormal()
        {
            List<KeyValuePair<string, bool>> sequence = new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>("p1", true),
                new KeyValuePair<string, bool>("p2", true),
                new KeyValuePair<string, bool>("p3", true)
            };

            Func<TestPredicateSet> constructor = () => new TestPredicateSet(sequence);

            var predicateSet = constructor.Should().NotThrow().Subject;
            predicateSet.Should().NotBeNull();
            predicateSet.Predicates.Should().BeEquivalentTo(sequence);
        }

        [TestMethod]
        public void PredicateSet_Constructor_ShouldThrowWhenSequenceContainsNullPredicateName()
        {
            List<KeyValuePair<string, bool>> sequence = new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>(null, true)
            };

            Func<TestPredicateSet> constructor = () => new TestPredicateSet(sequence);

            constructor.Should()
                .Throw<ArgumentException>()
                .WithMessage("sequence contains an invalid predicate name (Parameter 'predicates')")
                .And
                .ParamName.Should().Be("predicates");
        }

        [TestMethod]
        public void PredicateSet_Constructor_ShouldThrowWhenSequenceContainsEmptyPredicateName()
        {
            List<KeyValuePair<string, bool>> sequence = new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>("", true)
            };

            Func<TestPredicateSet> constructor = () => new TestPredicateSet(sequence);

            constructor.Should()
                .Throw<ArgumentException>()
                .WithMessage("sequence contains an invalid predicate name (Parameter 'predicates')")
                .And
                .ParamName.Should().Be("predicates");
        }

        [TestMethod]
        public void PredicateSet_Constructor_ShouldThrowWhenSequenceContainsDuplicatePredicateName()
        {
            List<KeyValuePair<string, bool>> sequence = new List<KeyValuePair<string, bool>>
            {
                new KeyValuePair<string, bool>("p1", true),
                new KeyValuePair<string, bool>("p1", false)
            };

            Func<TestPredicateSet> constructor = () => new TestPredicateSet(sequence);

            constructor.Should()
                .Throw<ArgumentException>()
                .WithMessage("sequence contains a duplicate predicate name (Parameter 'predicates')")
                .And
                .ParamName.Should().Be("predicates");
        }

        [TestMethod]
        public void PredicateSet_Equality_NullEqualsNull()
        {
            PredicateSet predicateSet1 = null;
            PredicateSet predicateSet2 = null;

            (predicateSet1 == predicateSet2).Should().BeTrue();
            (predicateSet1 != predicateSet2).Should().BeFalse();
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldNotBeEqualToNull()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            predicateSet.Equals(null as object).Should().BeFalse();
            predicateSet.Equals(null as PredicateSet).Should().BeFalse();
            (predicateSet == null).Should().BeFalse();
            (predicateSet != null).Should().BeTrue();

            // check with null on the other side too for operators
            (null == predicateSet).Should().BeFalse();
            (null != predicateSet).Should().BeTrue();
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldBeEqualToSameObject()
        {
            var predicateSet1 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });
            var predicateSet2 = predicateSet1;

            predicateSet1.Equals(predicateSet2 as object).Should().BeTrue();
            predicateSet1.Equals(predicateSet2 as PredicateSet).Should().BeTrue();
            (predicateSet1 == predicateSet2).Should().BeTrue();
            (predicateSet1 != predicateSet2).Should().BeFalse();

            // Equal predicate sets must have the same hashcode.
            predicateSet1.GetHashCode().Should().Be(predicateSet2.GetHashCode());
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldNotBeEqualToDifferentType()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            predicateSet.Equals("test").Should().BeFalse();
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldBeEqualToEquivalentSameOrder()
        {
            var predicateSet1 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            var predicateSet2 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            // Just asserting the obvious
            Object.ReferenceEquals(predicateSet1, predicateSet2).Should().BeFalse();

            predicateSet1.Equals(predicateSet2 as object).Should().BeTrue();
            predicateSet1.Equals(predicateSet2 as PredicateSet).Should().BeTrue();
            (predicateSet1 == predicateSet2).Should().BeTrue();
            (predicateSet1 != predicateSet2).Should().BeFalse();

            // Equal predicate sets must have the same hashcode.
            predicateSet1.GetHashCode().Should().Be(predicateSet2.GetHashCode());
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldBeEqualToEquivalentDifferentOrder()
        {
            var predicateSet1 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            var predicateSet2 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p3"] = true,
                    ["p2"] = false,
                    ["p1"] = true
                });

            // Just asserting the obvious
            Object.ReferenceEquals(predicateSet1, predicateSet2).Should().BeFalse();

            predicateSet1.Equals(predicateSet2 as object).Should().BeTrue();
            predicateSet1.Equals(predicateSet2 as PredicateSet).Should().BeTrue();
            (predicateSet1 == predicateSet2).Should().BeTrue();
            (predicateSet1 != predicateSet2).Should().BeFalse();

            // Equal predicate sets must have the same hashcode.
            predicateSet1.GetHashCode().Should().Be(predicateSet2.GetHashCode());
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldNotBeEqualToDifferentKeys()
        {
            var predicateSet1 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            var predicateSet2 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p2"] = false,
                    ["p3"] = true,
                    ["p4"] = true
                });

            // Just asserting the obvious
            Object.ReferenceEquals(predicateSet1, predicateSet2).Should().BeFalse();

            predicateSet1.Equals(predicateSet2 as object).Should().BeFalse();
            predicateSet1.Equals(predicateSet2 as PredicateSet).Should().BeFalse();
            (predicateSet1 == predicateSet2).Should().BeFalse();
            (predicateSet1 != predicateSet2).Should().BeTrue();

            // Non-equal predicate sets could still have the same hashcode.
        }

        [TestMethod]
        public void PredicateSet_Equality_ShouldNotBeEqualToDifferentValues()
        {
            var predicateSet1 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true
                });

            var predicateSet2 = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = true, // different value
                    ["p3"] = true
                });

            // Just asserting the obvious
            Object.ReferenceEquals(predicateSet1, predicateSet2).Should().BeFalse();

            predicateSet1.Equals(predicateSet2 as object).Should().BeFalse();
            predicateSet1.Equals(predicateSet2 as PredicateSet).Should().BeFalse();
            (predicateSet1 == predicateSet2).Should().BeFalse();
            (predicateSet1 != predicateSet2).Should().BeTrue();

            // Non-equal predicate sets could still have the same hashcode.
        }

        [TestMethod]
        public void PredicateSet_Contains_ShouldContainExpectedPredicates()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true,
                }
            );

            predicateSet.Contains("p1").Should().BeTrue();
            predicateSet.Contains("p2").Should().BeTrue();
            predicateSet.Contains("p3").Should().BeTrue();
        }

        [TestMethod]
        public void PredicateSet_Contains_ShouldNotContainUnexpectedPredicates()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true,
                }
            );

            predicateSet.Contains("p0").Should().BeFalse();
            predicateSet.Contains("p4").Should().BeFalse();
        }

        [TestMethod]
        public void PredicateSet_Contains_ShouldNotContainNull()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true,
                }
            );

            predicateSet.Contains(null).Should().BeFalse();
        }

        [TestMethod]
        public void PredicateSet_Indexer_ShouldReturnExpectedPredicates()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true,
                }
            );

            predicateSet["p1"].Should().BeTrue();
            predicateSet["p2"].Should().BeFalse();
            predicateSet["p3"].Should().BeTrue();
        }

        [TestMethod]
        public void PredicateSet_Indexer_ShouldNotContainUnexpectedPredicates()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true,
                }
            );

            predicateSet.Invoking((ps) => ps["p0"]).Should().Throw<KeyNotFoundException>();
        }

        [TestMethod]
        public void PredicateSet_Indexer_ShouldFailWhenPredicateIsNull()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p1"] = true,
                    ["p2"] = false,
                    ["p3"] = true,
                }
            );

            predicateSet.Invoking((ps) => ps[null]).Should()
                .Throw<ArgumentNullException>()
                .WithMessage("predicate cannot be null (Parameter 'predicate')")
                .And
                .ParamName.Should().Be("predicate");
        }

        [TestMethod]
        public void PredicateSet_Predicates_ShouldBeEmptyWhenThereAreNoPredicates()
        {
            var predicateSet = new TestPredicateSet(null);

            predicateSet.Predicates.Should().NotBeNull();
            predicateSet.Predicates.Should().BeEmpty();
        }

        [TestMethod]
        public void PredicateSet_Predicates_ShouldBeSorted()
        {
            var predicateSet = new TestPredicateSet(
                new Dictionary<string, bool>
                {
                    ["p3"] = true,
                    ["p2"] = false,
                    ["p1"] = true,
                }
            );

            var comparer = Comparer<KeyValuePair<string, bool>>.Create(
                (a, b) => string.CompareOrdinal(a.Key, b.Key));

            predicateSet.Predicates.Should().BeInAscendingOrder(comparer);
        }
    }
}
