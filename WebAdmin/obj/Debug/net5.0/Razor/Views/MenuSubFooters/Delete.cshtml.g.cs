#pragma checksum "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b56149d125f9a19825f761baf82743e9bad76afc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_MenuSubFooters_Delete), @"mvc.1.0.view", @"/Views/MenuSubFooters/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/MenuSubFooters/Delete.cshtml", typeof(AspNetCore.Views_MenuSubFooters_Delete))]
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
#line 1 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b56149d125f9a19825f761baf82743e9bad76afc", @"/Views/MenuSubFooters/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fdfc00b4acb27bf5f36fd09e3255c63eaaaa109", @"/Views/_ViewImports.cshtml")]
    public class Views_MenuSubFooters_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EntityFramework.Web.Entities.MenuSubFooter>
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
            BeginContext(78, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(129, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 6 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
  
    ViewData["Title"] = Localizer["Xóa"];

#line default
#line hidden
            BeginContext(177, 5, true);
            WriteLiteral("\n<h1>");
            EndContext();
            BeginContext(183, 16, false);
#line 10 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
Write(Localizer["Xóa"]);

#line default
#line hidden
            EndContext();
            BeginContext(199, 11, true);
            WriteLiteral("</h1>\n\n<h3>");
            EndContext();
            BeginContext(211, 46, false);
#line 12 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
Write(Localizer["Bạn có chắc muốn xóa bản ghi này?"]);

#line default
#line hidden
            EndContext();
            BeginContext(257, 20, true);
            WriteLiteral("</h3>\n<div>\n    <h4>");
            EndContext();
            BeginContext(278, 32, false);
#line 14 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
   Write(Localizer["Menu phụ chân trang"]);

#line default
#line hidden
            EndContext();
            BeginContext(310, 80, true);
            WriteLiteral("</h4>\n    <hr />\n    <dl class=\"row\">\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(391, 43, false);
#line 18 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.UrlText));

#line default
#line hidden
            EndContext();
            BeginContext(434, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(493, 39, false);
#line 21 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
       Write(Html.DisplayFor(model => model.UrlText));

#line default
#line hidden
            EndContext();
            BeginContext(532, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(590, 46, false);
#line 24 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.UrlAddress));

#line default
#line hidden
            EndContext();
            BeginContext(636, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(695, 42, false);
#line 27 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
       Write(Html.DisplayFor(model => model.UrlAddress));

#line default
#line hidden
            EndContext();
            BeginContext(737, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(795, 42, false);
#line 30 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(837, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(896, 38, false);
#line 33 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Status));

#line default
#line hidden
            EndContext();
            BeginContext(934, 15, true);
            WriteLiteral("\n        </dd>\n");
            EndContext();
            BeginContext(2394, 15, true);
            WriteLiteral("    </dl>\n\n    ");
            EndContext();
            BeginContext(2409, 233, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b56149d125f9a19825f761baf82743e9bad76afc8882", async() => {
                BeginContext(2435, 9, true);
                WriteLiteral("\n        ");
                EndContext();
                BeginContext(2444, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b56149d125f9a19825f761baf82743e9bad76afc9271", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 80 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
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
                BeginContext(2480, 29, true);
                WriteLiteral("\n        <input type=\"submit\"");
                EndContext();
                BeginWriteAttribute("value", " value=\"", 2509, "\"", 2534, 1);
#line 81 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
WriteAttributeValue("", 2517, Localizer["Xóa"], 2517, 17, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2535, 37, true);
                WriteLiteral(" class=\"btn btn-danger\" /> |\n        ");
                EndContext();
                BeginContext(2572, 58, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b56149d125f9a19825f761baf82743e9bad76afc11591", async() => {
                    BeginContext(2595, 31, false);
#line 82 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\MenuSubFooters\Delete.cshtml"
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
                BeginContext(2630, 5, true);
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
            BeginContext(2642, 8, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EntityFramework.Web.Entities.MenuSubFooter> Html { get; private set; }
    }
}
#pragma warning restore 1591