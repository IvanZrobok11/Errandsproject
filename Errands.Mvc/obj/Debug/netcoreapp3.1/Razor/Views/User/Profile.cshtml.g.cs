#pragma checksum "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3d00b1ff71063878619e986bea0e1bbc7cb087b4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Profile), @"mvc.1.0.view", @"/Views/User/Profile.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\_ViewImports.cshtml"
using Errands.Mvc.Models.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\_ViewImports.cshtml"
using Errands.Domain.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3d00b1ff71063878619e986bea0e1bbc7cb087b4", @"/Views/User/Profile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1ee6a9d37c2386311d1d1000e638549d55f9e42", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Profile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChangeInfo", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div>\r\n    Email: ");
#nullable restore
#line 5 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
      Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    Nickname: ");
#nullable restore
#line 6 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
         Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />\r\n    FirstName: ");
#nullable restore
#line 7 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
          Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n    LastName: ");
#nullable restore
#line 8 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
         Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n");
#nullable restore
#line 9 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
     if (Model.Path == null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <img src=\"/img/w128h1281338911651user.png\" alt=\"Alternate Text\" />\r\n");
#nullable restore
#line 12 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
    }
    else
	{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t    <img");
            BeginWriteAttribute("src", " src=\"", 315, "\"", 332, 1);
#nullable restore
#line 15 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
WriteAttributeValue("", 321, Model.Path, 321, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Alternate Text\" />\r\n");
#nullable restore
#line 16 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\User\Profile.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3d00b1ff71063878619e986bea0e1bbc7cb087b45843", async() => {
                WriteLiteral("ChangeInfo");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
