#pragma checksum "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bfd85c269497c16ee9723d0d3da5eacd7fd77f06"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Message_Chat), @"mvc.1.0.view", @"/Views/Message/Chat.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bfd85c269497c16ee9723d0d3da5eacd7fd77f06", @"/Views/Message/Chat.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1ee6a9d37c2386311d1d1000e638549d55f9e42", @"/Views/_ViewImports.cshtml")]
    public class Views_Message_Chat : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UserInfoViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/src/js/microsoft/signalr/dist/browser/signalr.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<br />\r\n<p>Nickname: ");
#nullable restore
#line 9 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
        Write(Model.Nickname);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>FirstName: ");
#nullable restore
#line 10 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
         Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>LastName: ");
#nullable restore
#line 11 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
        Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>ID recivers: ");
#nullable restore
#line 12 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
           Write(Model.ReciverUserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 13 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
 if (Model.PathLogo == null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <img src=\"/img/w128h1281338911651user.png\" alt=\"Alternate Text\" />\r\n");
#nullable restore
#line 16 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <img");
            BeginWriteAttribute("src", " src=\"", 529, "\"", 550, 1);
#nullable restore
#line 19 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
WriteAttributeValue("", 535, Model.PathLogo, 535, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"Alternate Text\" />\r\n");
#nullable restore
#line 20 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n\r\n<br />\r\n\r\n<div id=\"inputForm\">\r\n    <input type=\"text\" id=\"message\" placeholder=\"Введите сообщение\" />\r\n    <input type=\"text\" id=\"receiver\" placeholder=\"Введіть отримувача(необовязково)\" />\r\n    <input type=\"button\" id=\"sendBtn\" ");
            WriteLiteral(" value=\"Отправить\" />\r\n</div>\r\n<div id=\"chatroom\"></div>\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bfd85c269497c16ee9723d0d3da5eacd7fd77f066290", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<script>\r\n        let token = `");
#nullable restore
#line 34 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
                Write(Model.Token);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"`;
        const hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(""/messages"", { accessTokenFactory: () => token })
            .build();
        hubConnection.on(""Receive"", function (message, userName) {

            // создаем элемент <b> для имени пользователя
            let userNameElem = document.createElement(""b"");
            userNameElem.appendChild(document.createTextNode(userName + "": ""));

            // создает элемент <p> для сообщения пользователя
            let elem = document.createElement(""p"");
            elem.appendChild(userNameElem);
            elem.appendChild(document.createTextNode(message));

            var firstElem = document.getElementById(""chatroom"").firstChild;
            document.getElementById(""chatroom"").insertBefore(elem, firstElem);
        });
            hubConnection.start();

        // отправка сообщения на сервер
        document.getElementById(""sendBtn"").addEventListener(""click"", function (e) {
            let message ");
            WriteLiteral("= document.getElementById(\"message\").value;\r\n            let to = document.getElementById(\"receiver\").value;\r\n            if (to == \"\") {\r\n                to = `");
#nullable restore
#line 59 "C:\Users\LapStore\source\с#project\ErrandsProject\Errands.Mvc\Views\Message\Chat.cshtml"
                 Write(Model.ReciverUserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("`;\r\n            }\r\n            hubConnection.invoke(\"Send\", message, to);\r\n        });\r\n\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UserInfoViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
