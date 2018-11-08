// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.AspNetCore.Razor.Language.IntegrationTests;
using Xunit;

namespace Microsoft.AspNetCore.Razor.Tools
{
    public class EliminateMethodBodyPassIntegrationTest : IntegrationTestBase
    {
        public EliminateMethodBodyPassIntegrationTest()
            : base(generateBaselines: null)
        {
            Configuration = RazorConfiguration.Create(
                RazorLanguageVersion.Version_3_0,
                "Component-1.0",
                Enumerable.Empty<RazorExtension>());

            FileExtension = ".razor";
        }

        protected override RazorConfiguration Configuration { get; }

        [Fact]
        public void BasicTest()
        {
            var engine = CreateProjectEngine(b =>
            {
                b.Features.Add(new EliminateMethodBodyPass());
            });

            var projectItem = CreateProjectItemFromFile();

            // Act
            var codeDocument = engine.Process(projectItem);

            // Assert
            AssertDocumentNodeMatchesBaseline(codeDocument.GetDocumentIntermediateNode());

            var csharpDocument = codeDocument.GetCSharpDocument();
            AssertCSharpDocumentMatchesBaseline(csharpDocument);
            Assert.Empty(csharpDocument.Diagnostics);
        }
    }
}