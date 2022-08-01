namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test.PredicateBuilderExtensionsTest;

public class GreaterThan
{
    [TestCase]
    public void Test(int value, bool expected)
    {

    }

    public class GreaterThanValidator : Validator<TestModel<int>>
    {
        public GreaterThanValidator()
        {

        }
    }
}
