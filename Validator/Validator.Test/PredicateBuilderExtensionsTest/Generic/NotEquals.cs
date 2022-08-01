namespace Validator.Test.PredicateBuilderExtensionsTest.Generic;

using NUnit.Framework;

public class NotEquals
{
    [TestCase]
    public void Test(object value, bool expected)
    {

    }

    public class NotEqualsValidator : Validator<object>
    {
        public NotEqualsValidator()
        {

        }
    }
}
