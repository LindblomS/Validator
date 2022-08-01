namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test;

public class GreaterThanOrEqualsTo
{
    [TestCase(11, true)]
    [TestCase(10, true)]
    [TestCase(9, false)]
    [TestCase(-1, false)]
    [TestCase(int.MaxValue, true)]
    [TestCase(int.MinValue, false)]
    public void Test(int value, bool expected)
    {
        Assert.That(Helper.Validate<GreaterThanOrEqualsToValidator, int>(value).Valid == expected);
    }

    public class GreaterThanOrEqualsToValidator : Validator<TestModel<int>>
    {
        public GreaterThanOrEqualsToValidator()
        {
            For(model => model.Value).GreaterThanOrEqualsTo(10);
        }
    }
}
