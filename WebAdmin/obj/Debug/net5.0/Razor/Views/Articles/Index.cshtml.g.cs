#pragma checksum "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e9eba513741563c8421c669e1140cb87ca9896c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Articles_Index), @"mvc.1.0.view", @"/Views/Articles/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Articles/Index.cshtml", typeof(AspNetCore.Views_Articles_Index))]
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
#line 1 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
using Microsoft.AspNetCore.Mvc.Localization;

#line default
#line hidden
#line 4 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
using X.PagedList.Mvc.Core.Common;

#line default
#line hidden
#line 5 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#line 6 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
using X.PagedList;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9eba513741563c8421c669e1140cb87ca9896c9", @"/Views/Articles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5fdfc00b4acb27bf5f36fd09e3255c63eaaaa109", @"/Views/_ViewImports.cshtml")]
    public class Views_Articles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IPagedList<EntityFramework.Web.Entities.Article>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(78, 1, true);
            WriteLiteral("\n");
            EndContext();
            BeginContext(281, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 10 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
   ViewData["Title"] = Localizer["Danh sách"]; 

#line default
#line hidden
            BeginContext(331, 5, true);
            WriteLiteral("\n<h1>");
            EndContext();
            BeginContext(337, 22, false);
#line 12 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
Write(Localizer["Danh sách"]);

#line default
#line hidden
            EndContext();
            BeginContext(359, 15, true);
            WriteLiteral("</h1>\n\n<p>\n    ");
            EndContext();
            BeginContext(374, 49, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9eba513741563c8421c669e1140cb87ca9896c95767", async() => {
                BeginContext(398, 21, false);
#line 15 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                      Write(Localizer["Thêm mới"]);

#line default
#line hidden
                EndContext();
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
            EndContext();
            BeginContext(423, 6, true);
            WriteLiteral("\n</p>\n");
            EndContext();
#line 17 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
 using (Html.BeginForm())
{

#line default
#line hidden
            BeginContext(492, 66, true);
            WriteLiteral("                <div class=\"pagination\">\n                    Page ");
            EndContext();
            BeginContext(560, 57, false);
#line 21 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                     Write(Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber);

#line default
#line hidden
            EndContext();
            BeginContext(618, 24, true);
            WriteLiteral("\n                    of ");
            EndContext();
            BeginContext(643, 15, false);
#line 22 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                  Write(Model.PageCount);

#line default
#line hidden
            EndContext();
            BeginContext(658, 3, true);
            WriteLiteral("   ");
            EndContext();
            BeginContext(662, 200, false);
#line 22 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                     Write(Html.PagedListPager(Model, page =>
Url.Action("Index", new { page = page }),
new PagedListRenderOptions
{
LiElementClasses = new string[] { "page-item" },
PageClasses = new string[] { "page-link" }
}));

#line default
#line hidden
            EndContext();
            BeginContext(862, 264, true);
            WriteLiteral(@"
                </div>
                                <table class=""table"">
                                    <thead>
                                        <tr>
                                            <th>
                                                ");
            EndContext();
            BeginContext(1127, 53, false);
#line 34 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].NewsCategories));

#line default
#line hidden
            EndContext();
            BeginContext(1180, 148, true);
            WriteLiteral("\n                                            </th>\n                                            <th>\n                                                ");
            EndContext();
            BeginContext(1329, 44, false);
#line 37 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].Title));

#line default
#line hidden
            EndContext();
            BeginContext(1373, 148, true);
            WriteLiteral("\n                                            </th>\n                                            <th>\n                                                ");
            EndContext();
            BeginContext(1522, 42, false);
#line 40 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].Img));

#line default
#line hidden
            EndContext();
            BeginContext(1564, 148, true);
            WriteLiteral("\n                                            </th>\n                                            <th>\n                                                ");
            EndContext();
            BeginContext(1713, 46, false);
#line 43 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].Summary));

#line default
#line hidden
            EndContext();
            BeginContext(1759, 148, true);
            WriteLiteral("\n                                            </th>\n                                            <th>\n                                                ");
            EndContext();
            BeginContext(1908, 45, false);
#line 46 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].IsNews));

#line default
#line hidden
            EndContext();
            BeginContext(1953, 148, true);
            WriteLiteral("\n                                            </th>\n                                            <th>\n                                                ");
            EndContext();
            BeginContext(2102, 45, false);
#line 49 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].Status));

#line default
#line hidden
            EndContext();
            BeginContext(2147, 148, true);
            WriteLiteral("\n                                            </th>\n                                            <th>\n                                                ");
            EndContext();
            BeginContext(2296, 48, false);
#line 52 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model[0].Publisher));

#line default
#line hidden
            EndContext();
            BeginContext(2344, 51, true);
            WriteLiteral("\n                                            </th>\n");
            EndContext();
            BeginContext(3193, 189, true);
            WriteLiteral("                                            <th></th>\n                                        </tr>\n                                    </thead>\n                                    <tbody>\n");
            EndContext();
#line 70 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                         foreach (var item in Model)
                                        {

#line default
#line hidden
            BeginContext(3493, 94, true);
            WriteLiteral("                        <tr>\n                            <td>\n                                ");
            EndContext();
            BeginContext(3588, 54, false);
#line 74 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.NewsCategories.Name));

#line default
#line hidden
            EndContext();
            BeginContext(3642, 100, true);
            WriteLiteral("\n                            </td>\n                            <td>\n                                ");
            EndContext();
            BeginContext(3743, 40, false);
#line 77 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Title));

#line default
#line hidden
            EndContext();
            BeginContext(3783, 100, true);
            WriteLiteral("\n                            </td>\n                            <td>\n                                ");
            EndContext();
            BeginContext(3884, 38, false);
#line 80 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Img));

#line default
#line hidden
            EndContext();
            BeginContext(3922, 100, true);
            WriteLiteral("\n                            </td>\n                            <td>\n                                ");
            EndContext();
            BeginContext(4023, 42, false);
#line 83 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Summary));

#line default
#line hidden
            EndContext();
            BeginContext(4065, 100, true);
            WriteLiteral("\n                            </td>\n                            <td>\n                                ");
            EndContext();
            BeginContext(4166, 41, false);
#line 86 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.IsNews));

#line default
#line hidden
            EndContext();
            BeginContext(4207, 100, true);
            WriteLiteral("\n                            </td>\n                            <td>\n                                ");
            EndContext();
            BeginContext(4308, 41, false);
#line 89 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
            EndContext();
            BeginContext(4349, 100, true);
            WriteLiteral("\n                            </td>\n                            <td>\n                                ");
            EndContext();
            BeginContext(4450, 44, false);
#line 92 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                           Write(Html.DisplayFor(modelItem => item.Publisher));

#line default
#line hidden
            EndContext();
            BeginContext(4494, 35, true);
            WriteLiteral("\n                            </td>\n");
            EndContext();
            BeginContext(5119, 65, true);
            WriteLiteral("                            <td>\n                                ");
            EndContext();
            BeginContext(5184, 66, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9eba513741563c8421c669e1140cb87ca9896c917054", async() => {
                BeginContext(5230, 16, false);
#line 107 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                                                        Write(Localizer["Sửa"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 107 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                                       WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5250, 35, true);
            WriteLiteral(" |\n                                ");
            EndContext();
            BeginContext(5285, 78, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9eba513741563c8421c669e1140cb87ca9896c919616", async() => {
                BeginContext(5334, 25, false);
#line 108 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                                                           Write(Localizer["Xem chi tiết"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 108 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                                          WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5363, 35, true);
            WriteLiteral(" |\n                                ");
            EndContext();
            BeginContext(5398, 68, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e9eba513741563c8421c669e1140cb87ca9896c922193", async() => {
                BeginContext(5446, 16, false);
#line 109 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                                                          Write(Localizer["Xóa"]);

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 109 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                                                         WriteLiteral(item.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(5466, 64, true);
            WriteLiteral("\n                            </td>\n                        </tr>");
            EndContext();
#line 111 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                             }

#line default
#line hidden
            BeginContext(5532, 86, true);
            WriteLiteral("                                    </tbody>\n                                </table> ");
            EndContext();
            BeginContext(5826, 154, true);
            WriteLiteral("                                                <p></p>\n                                                                <div class=\"pagination\">\n    Page ");
            EndContext();
            BeginContext(5982, 57, false);
#line 119 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
     Write(Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber);

#line default
#line hidden
            EndContext();
            BeginContext(6040, 8, true);
            WriteLiteral("\n    of ");
            EndContext();
            BeginContext(6049, 15, false);
#line 120 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
  Write(Model.PageCount);

#line default
#line hidden
            EndContext();
            BeginContext(6064, 3, true);
            WriteLiteral("   ");
            EndContext();
            BeginContext(6068, 200, false);
#line 120 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
                     Write(Html.PagedListPager(Model, page =>
Url.Action("Index", new { page = page }),
new PagedListRenderOptions
{
LiElementClasses = new string[] { "page-item" },
PageClasses = new string[] { "page-link" }
}));

#line default
#line hidden
            EndContext();
            BeginContext(6268, 7, true);
            WriteLiteral("\n</div>");
            EndContext();
#line 127 "D:\Project\00.NgocTuan\Project\Application\WebAdmin\Views\Articles\Index.cshtml"
      }

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IPagedList<EntityFramework.Web.Entities.Article>> Html { get; private set; }
    }
}
#pragma warning restore 1591