#region MIT
/*
Copyright (c) 2008 Michael Woerister

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Platinum
{
    public interface IValidator
    {
        /// <summary>
        /// Returns the validated value or null if the value was not valid.
        /// </summary>
        /// <param name="o">The Object to validate</param>
        /// <returns>The type of the returned object must be the same as ValidatedType</returns>
        Object ValidateValue( Object o );

        /// <summary>
        /// Tries to convert an object of the validated type to the given type T.
        /// </summary>
        /// <typeparam name="T">The target type of the conversion.</typeparam>
        /// <param name="o">The object of the validated type that should be converted.</param>
        /// <returns>The converted object or null if the conversion failed.</returns>
        T ConvertTo<T>( Object o ) where T : class;

        /// <summary>
        /// The type validated by this validator.
        /// </summary>
        Type ValidatedType { get; }

        /// <summary>
        /// An error message specifying the last error. Empty if the last validation
        /// was successful.
        /// </summary>
        String Message { get; }

        /// <summary>
        /// A default value of the validated type. This default value must always
        /// pass validation.
        /// </summary>
        Object DefaultValue { get; }
    }
}
