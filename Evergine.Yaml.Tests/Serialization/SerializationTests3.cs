// Copyright (c) 2015 SharpYaml - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// -------------------------------------------------------------------------------
// SharpYaml is a fork of YamlDotNet https://github.com/aaubry/YamlDotNet
// published with the following license:
// -------------------------------------------------------------------------------
//
// Copyright (c) 2008, 2009, 2010, 2011, 2012 Antoine Aubry
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do
// so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using NUnit.Framework;
using SharpYaml.Serialization;
using SharpYaml.Serialization.Serializers;

namespace Evergine.Yaml.Tests.Serialization
{
    [TestFixture]
    public class SerializationTests3
    {
        public class ClassA
        {
            public string A;
            public string B;
        }

        public class ClassB
        {
            [YamlMember]
            public ClassA PropertyA { get; }

            public ClassB(ClassA a)
            {
                this.PropertyA = a;
            }
        }

        [Test]
        public void NonRecurrentSerializations()
        {
            var settings = new SerializerSettings()
            {
                EmitAlias = true,
            };

            var a = new ClassA()
            {
                A = "1",
                B = "2"
            };

            var serializer = new Serializer(settings);

            var s1 = serializer.Serialize(a);

            Assert.IsNotEmpty(s1);

            var s2 = serializer.Serialize(a);

            Assert.AreNotEqual(s1, s2);
        }

        [Test]
        public void RecurrentSerializations()
        {
            var settings = new SerializerSettings()
            {
                EmitAlias = true,
                ResetAlias = true,
            };

            var a = new ClassA()
            {
                A = "1",
                B = "2"
            };

            var serializer = new Serializer(settings);

            var s1 = serializer.Serialize(a);

            Assert.IsNotEmpty(s1);

            var s2 = serializer.Serialize(a);

            Assert.AreEqual(s1, s2);
        }

        [Test]
        public void IgnoreGetters()
        {
            var settings = new SerializerSettings()
            {
                EmitAlias = true,
                ResetAlias = true,
                IgnoreGetters = true,
            };

            var a = new ClassA()
            {
                A = "1",
                B = "2"
            };

            var b = new ClassB(a);

            var serializer = new Serializer(settings);

            var s1 = serializer.Serialize(b);
        }
    }
}