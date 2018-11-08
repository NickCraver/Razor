// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Razor.Language.Components;
using Xunit;

namespace Microsoft.AspNetCore.Razor.Language.IntegrationTests
{
    public class ComponentIntegrationTest : IntegrationTestBase
    {
        public ComponentIntegrationTest()
            : base(generateBaselines: null)
        {
            Configuration = RazorConfiguration.Create(
                RazorLanguageVersion.Version_3_0,
                ComponentExtensions.ConfigurationName,
                Enumerable.Empty<RazorExtension>());

            FileExtension = ".razor";
        }

        protected override RazorConfiguration Configuration { get; }

        [Fact]
        public void BasicTest()
        {
            var projectEngine = CreateProjectEngine();
            var projectItem = CreateProjectItemFromFile();

            // Act
            var codeDocument = projectEngine.Process(projectItem);

            // Assert
            AssertDocumentNodeMatchesBaseline(codeDocument.GetDocumentIntermediateNode());
            AssertCSharpDocumentMatchesBaseline(codeDocument.GetCSharpDocument());
        }
    }
}
