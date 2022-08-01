namespace Validator.Test.PredicateBuilderExtensionsTest.Integers;

using NUnit.Framework;
using Validator.Test.PredicateBuilderExtensionsTest;

public class LessThan
{
    [TestCase]
    public void Test(int value, bool expected)
    {

    }

    public class LessThanValidator : Validator<TestModel<int>>
    {
        public LessThanValidator()
        {

        }
    }
}
