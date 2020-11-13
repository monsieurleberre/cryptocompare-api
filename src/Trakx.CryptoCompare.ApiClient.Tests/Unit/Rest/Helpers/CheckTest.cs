﻿using System;
using Trakx.CryptoCompare.ApiClient.Rest.Helpers;
using Xunit;

namespace Trakx.CryptoCompare.ApiClient.Tests.Unit.Rest.Helpers
{
    public class CheckTest
    {
        public static string Blah = nameof(Blah);

        /// <summary>
        /// NotNullOrWhiteSpace should not throw ArgumentNullException when string is not null.
        /// </summary>
        [Fact]
        public void NotNullOrWhiteSpaceShouldNotThrowArgumentNullExceptionWhenStringIsNotNull()
        {
            Check.NotNullOrWhiteSpace(Blah, Blah);
        }

        /// <summary>
        /// NotNullOrWhiteSpace should throw ArgumentNullException when string is null or empty or whitespace.
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void NotNullOrWhiteSpaceShouldThrowArgumentNullExceptionWhenStringIsNullOrEmptyOrWhitespace(string value)
        {
            var exception = Assert.Throws<ArgumentNullException>(() => Check.NotNullOrWhiteSpace(value, Blah));
            Assert.Equal(exception.ParamName, Blah);
        }

        /// <summary>
        /// NotNull should not throw ArgumentNullException when object is not null.
        /// </summary>
        [Fact]
        public void NotNullShouldNotThrowArgumentNullExceptionWhenObjectIsNotNull()
        {
            Check.NotNull<int?>(1, Blah);
        }

        /// <summary>
        /// NotNull should throw ArgumentNullException when object is null.
        /// </summary>
        [Fact]
        public void NotNullShouldThrowArgumentNullExceptionWhenObjectIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => Check.NotNull<int?>(null, Blah));
            Assert.Equal(exception.ParamName, Blah);
        }
    }
}
