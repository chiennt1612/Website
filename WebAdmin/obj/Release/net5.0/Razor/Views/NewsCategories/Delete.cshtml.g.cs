#pragma checksum "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dc442927016a12bc8816e748a0ab520c967d0fba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_NewsCategories_Delete), @"mvc.1.0.view", @"/Views/NewsCategories/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/NewsCategories/Delete.cshtml", typeof(AspNetCore.Views_NewsCategories_Delete))]
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
#line 1 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\_ViewImports.cshtml"
using WebAdmin;

#line default
#line hidden
#line 2 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\_ViewImports.cshtml"
using WebAdmin.Models;

#line default
#line hidden
#line 1 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dc442927016a12bc8816e748a0ab520c967d0fba", @"/Views/NewsCategories/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fdfc00b4acb27bf5f36fd09e3255c63eaaaa109", @"/Views/_ViewImports.cshtml")]
    public class Views_NewsCategories_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebAdmin.Entities.NewsCategories>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(118, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 5 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
  
    ViewData["Title"] = Localizer["Xóa"];

#line default
#line hidden
            BeginContext(166, 5, true);
            WriteLiteral("\n<h1>");
            EndContext();
            BeginContext(172, 16, false);
#line 9 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
Write(Localizer["Xóa"]);

#line default
#line hidden
            EndContext();
            BeginContext(188, 11, true);
            WriteLiteral("</h1>\n\n<h3>");
            EndContext();
            BeginContext(200, 46, false);
#line 11 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
Write(Localizer["Bạn có chắc muốn xóa bản ghi này?"]);

#line default
#line hidden
            EndContext();
            BeginContext(246, 6, true);
            WriteLiteral("</h3> ");
            EndContext();
            BeginContext(293, 15, true);
            WriteLiteral("\n<div>\n    <h4>");
            EndContext();
            BeginContext(309, 25, false);
#line 13 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
   Write(Localizer["Nhóm tin tức"]);

#line default
#line hidden
            EndContext();
            BeginContext(334, 80, true);
            WriteLiteral("</h4>\n    <hr />\n    <dl class=\"row\">\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(415, 40, false);
#line 17 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(455, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(514, 36, false);
#line 20 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(550, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(608, 42, false);
#line 23 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Parent));

#line default
#line hidden
            EndContext();
            BeginContext(650, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(709, 43, false);
#line 26 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Parent.Name));

#line default
#line hidden
            EndContext();
            BeginContext(752, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(810, 42, false);
#line 29 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(852, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(911, 38, false);
#line 32 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(949, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(1007, 53, false);
#line 35 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DisplayOnMenuMain));

#line default
#line hidden
            EndContext();
            BeginContext(1060, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(1119, 49, false);
#line 38 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.DisplayOnMenuMain));

#line default
#line hidden
            EndContext();
            BeginContext(1168, 15, true);
            WriteLiteral("\n        </dd>\n");
            EndContext();
            BeginContext(2220, 42, true);
            WriteLiteral("        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(2263, 47, false);
#line 71 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.UserCreator));

#line default
#line hidden
            EndContext();
            BeginContext(2310, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(2369, 43, false);
#line 74 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.UserCreator));

#line default
#line hidden
            EndContext();
            BeginContext(2412, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(2470, 47, false);
#line 77 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DateCreator));

#line default
#line hidden
            EndContext();
            BeginContext(2517, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(2576, 43, false);
#line 80 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.DateCreator));

#line default
#line hidden
            EndContext();
            BeginContext(2619, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(2677, 46, false);
#line 83 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.UserModify));

#line default
#line hidden
            EndContext();
            BeginContext(2723, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(2782, 42, false);
#line 86 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.UserModify));

#line default
#line hidden
            EndContext();
            BeginContext(2824, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(2882, 46, false);
#line 89 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.DateModify));

#line default
#line hidden
            EndContext();
            BeginContext(2928, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(2987, 42, false);
#line 92 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
       Write(Html.DisplayFor(model => model.DateModify));

#line default
#line hidden
            EndContext();
            BeginContext(3029, 30, true);
            WriteLiteral("\n        </dd>\n    </dl>\n\n    ");
            EndContext();
            BeginContext(3059, 233, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dc442927016a12bc8816e748a0ab520c967d0fba13128", async() => {
                BeginContext(3085, 9, true);
                WriteLiteral("\n        ");
                EndContext();
                BeginContext(3094, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "dc442927016a12bc8816e748a0ab520c967d0fba13518", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 97 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Id);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3130, 29, true);
                WriteLiteral("\n        <input type=\"submit\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 3159, "\"", 3184, 1);
#line 98 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
WriteAttributeValue("", 3167, Localizer["Xóa"], 3167, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3185, 37, true);
                WriteLiteral(" class=\"btn btn-danger\" /> |\n        ");
                EndContext();
                BeginContext(3222, 58, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dc442927016a12bc8816e748a0ab520c967d0fba15839", async() => {
                    BeginContext(3245, 31, false);
#line 99 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\NewsCategories\Delete.cshtml"
                         Write(Localizer["Quay lại danh sách"]);

#line default
#line hidden
                    EndContext();
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(3280, 5, true);
                WriteLiteral("\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3292, 8, true);
            WriteLiteral("\n</div>\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IViewLocalizer Localizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebAdmin.Entities.NewsCategories> Html { get; private set; }
    }
}
#pragma warning restore 1591
