namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test.PredicateBuilderExtensionsTest;

public class LessThanOrEqualsTo
{
    [TestCase]
    public void Test(int value, bool expected)
    {

    }

    public class LessThanOrEqualsToValidator : Validator<TestModel<int>>
    {
        public LessThanOrEqualsToValidator()
        {

        }
    }
}
