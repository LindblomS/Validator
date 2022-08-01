namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test.PredicateBuilderExtensionsTest;

public class GreaterThanOrEqualsTo
{
    [TestCase]
    public void Test(int value, bool expected)
    {

    }

    public class GreaterThanOrEqualsToValidator : Validator<TestModel<int>>
    {
        public GreaterThanOrEqualsToValidator()
        {

        }
    }
}
