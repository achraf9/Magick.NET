﻿// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#if !NETCORE

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ThePdfCoder
    {
        private delegate void ReadDelegate();

        [TestMethod]
        public void ShouldReadFileMultithreadedCorrectly()
        {
            ReadDelegate action = () =>
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read(Files.Coders.CartoonNetworkStudiosLogoAI);

                    Assert.AreEqual(765, image.Width);
                    Assert.AreEqual(361, image.Height);
                    Assert.AreEqual(MagickFormat.Ai, image.Format);
                }
            };

            IAsyncResult[] results = new IAsyncResult[3];

            for (int i = 0; i < results.Length; ++i)
            {
                results[i] = action.BeginInvoke(null, null);
            }

            for (int i = 0; i < results.Length; ++i)
            {
                action.EndInvoke(results[i]);
            }
        }
    }
}

#endif