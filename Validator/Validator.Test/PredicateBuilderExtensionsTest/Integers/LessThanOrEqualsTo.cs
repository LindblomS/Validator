namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test;

public class LessThanOrEqualsTo
{
    [TestCase(11, false)]
    [TestCase(10, true)]
    [TestCase(9, true)]
    [TestCase(-1, true)]
    [TestCase(int.MinValue, true)]
    [TestCase(int.MaxValue, false)]
    public void Test(int value, bool expected)
    {
        Assert.That(Helper.Validate<LessThanOrEqualsToValidator, int>(value).Valid == expected);
    }

    public class LessThanOrEqualsToValidator : Validator<TestModel<int>>
    {
        public LessThanOrEqualsToValidator()
        {
            For(model => model.Value).LessThanOrEqualsTo(10);
        }
    }
}
