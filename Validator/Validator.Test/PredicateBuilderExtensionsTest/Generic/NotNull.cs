namespace Validator.Test.PredicateBuilderExtensionsTest.Generic;

using NUnit.Framework;

public class NotNull
{
    [TestCase]
    public void Test(object value, bool expected)
    {

    }

    public class NotNullValidator : Validator<object>
    {
        public NotNullValidator()
        {

        }
    }
}