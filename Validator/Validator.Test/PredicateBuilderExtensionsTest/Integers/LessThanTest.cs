namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test;

public class LessThanTest
{
    [TestCase(11, false)]
    [TestCase(10, false)]
    [TestCase(9, true)]
    [TestCase(-1, true)]
    [TestCase(int.MaxValue, false)]
    [TestCase(int.MinValue, true)]
    public void Test(int value, bool expected)
    {
        Assert.That(Helper.Validate<LessThanValidator, int>(value).Valid == expected);
    }

    public class LessThanValidator : Validator<TestModel<int>>
    {
        public LessThanValidator()
        {
            For(model => model.Value).LessThan(10);
        }
    }
}
