#pragma checksum "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "90064188b7b2636f294dd7f21b24e2951e2659c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_AsignacionTareas_Index), @"mvc.1.0.view", @"/Views/AsignacionTareas/Index.cshtml")]
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
#line 1 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\_ViewImports.cshtml"
using TallerHernandez;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\_ViewImports.cshtml"
using TallerHernandez.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"90064188b7b2636f294dd7f21b24e2951e2659c0", @"/Views/AsignacionTareas/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1b7e356a25368190d6b799e21830ceead35565db", @"/Views/_ViewImports.cshtml")]
    public class Views_AsignacionTareas_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<TallerHernandez.Models.AsignacionTarea>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CrearAsignacion", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-item"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "false", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "true", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row align\">\r\n    <div class=\"col-md-3 col-sm-12\">\r\n        <img src=\"/images/logoTaller.png\" style=\"max-height: 90%; max-width:100%;\">\r\n    </div>\r\n    <div class=\"col-md-9 col-sm-12 mb-5\">\r\n        <h1>Asignación de Tareas</h1>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c06455", async() => {
                WriteLiteral("\r\n            <div class=\"form-actions no-color\">\r\n                <p>\r\n                    Filtro: <input class=\"col-md-5\" type=\"text\" placeholder=\"Ingrese la placa del automóvil o nombre del encargado\" name=\"cadena\"");
                BeginWriteAttribute("value", " value=\"", 608, "\"", 635, 1);
#nullable restore
#line 15 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
WriteAttributeValue("", 616, ViewData["Filtro"], 616, 19, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                    <input type=\"submit\" value=\"Buscar\" class=\"btn btn-secondary\" />\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c07454", async() => {
                    WriteLiteral("Todos los registros");
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
                WriteLiteral("\r\n                </p>\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c010148", async() => {
                WriteLiteral("Crear Nuevo");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
</p>
<table class=""table table-striped table-responsive-sm"">
    <thead class=""thead-dark text-center"">
        <tr>
            <th>
                Automóvil
            </th>
            <th>
                Mantenimiento
            </th>
            <th>
                Procedimiento
            </th>
            <th>
                Encargado
            </th>
            <th>
                Entrega
            </th>
            <th>
                Estado
            </th>
            <th>
                <div class=""dropdown"">
                    <button class=""btn btn-secondary dropdown-toggle"" type=""button"" id=""dropdownMenuButton"" data-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                        Ordenar Por:
                    </button>
                    <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuButton"">
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c012251", async() => {
                WriteLiteral("      ");
#nullable restore
#line 53 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                                                                                                                  Write(Html.DisplayNameFor(model => model.recepcion.automovilID));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-OrdenAsig", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 53 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                                                                             WriteLiteral(ViewData["OrdenAuto"]);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["OrdenAsig"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-OrdenAsig", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["OrdenAsig"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c014952", async() => {
                WriteLiteral("     ");
#nullable restore
#line 54 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                                                                                                                Write(Html.DisplayNameFor(model => model.empleado.nombre));

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-OrdenAsig", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 54 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                                                                             WriteLiteral(ViewData["OrdenNom"]);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["OrdenAsig"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-OrdenAsig", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["OrdenAsig"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </div>\r\n            </th>\r\n        </tr>\r\n    </thead>\r\n    <tbody class=\"thead-dark text-center\">\r\n");
#nullable restore
#line 61 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 65 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.recepcion.automovilID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 68 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.recepcion.mantenimiento.nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 71 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.recepcion.procedimiento.procedimiento));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 74 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.empleado.nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 77 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
               Write(Html.DisplayFor(modelItem => item.recepcion.fechaSalida));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n");
#nullable restore
#line 79 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                 if (item.estadoTarea.Equals(false))
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        No finalizada\r\n                    </td>\r\n");
#nullable restore
#line 84 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        Finalizada\r\n                    </td>\r\n");
#nullable restore
#line 90 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            <td>\r\n                <a class=\"p-2\" style=\"color:#e2df37;\" data-toggle=\"modal\" data-target=\"#modalEditarAsignacion\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3497, "\"", 3590, 3);
            WriteAttributeValue("", 3507, "GetAsignacionTarea(\'", 3507, 20, true);
#nullable restore
#line 92 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
WriteAttributeValue("", 3527, item.asignacionTareaID, 3527, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3550, "\',\'AsignacionTareas/GetAsignacionTarea\')", 3550, 40, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"far fa-edit\"></i></a>\r\n                <a class=\"p-2\" style=\"color:#1985a8;\" data-toggle=\"modal\" data-target=\"#modalDetalleAsignacion\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3736, "\"", 3829, 3);
            WriteAttributeValue("", 3746, "GetAsignacionTarea(\'", 3746, 20, true);
#nullable restore
#line 93 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
WriteAttributeValue("", 3766, item.asignacionTareaID, 3766, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3789, "\',\'AsignacionTareas/GetAsignacionTarea\')", 3789, 40, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-info-circle\"></i></a>\r\n                <a class=\"p-2\" style=\"color:#ff0000;\" data-toggle=\"modal\" data-target=\"#modalEliminarAsignacion\"");
            BeginWriteAttribute("onclick", " onclick=\"", 3983, "\"", 4076, 3);
            WriteAttributeValue("", 3993, "GetAsignacionTarea(\'", 3993, 20, true);
#nullable restore
#line 94 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
WriteAttributeValue("", 4013, item.asignacionTareaID, 4013, 23, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 4036, "\',\'AsignacionTareas/GetAsignacionTarea\')", 4036, 40, true);
            EndWriteAttribute();
            WriteLiteral("><i class=\"fas fa-trash-alt\"></i></a>\r\n            </td>\r\n            </tr>\r\n");
#nullable restore
#line 97 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </tbody>
</table>

<!----Modal Eliminar-->
<div class=""modal fade"" id=""modalEliminarAsignacion"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4");
            BeginWriteAttribute("class", " class=\"", 4474, "\"", 4482, 0);
            EndWriteAttribute();
            WriteLiteral(@">Esta seguro de eliminar la asignación de tarea?</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" arial-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""form-group"">
                    <label for=""automovilID"" class=""control-label""><strong>Automóvil</strong></label>
                    <p id=""automovilID"">Placa automóvil</p>
                </div>
                <div class=""form-group"">
                    <label for=""nomMantenimiento"" class=""control-label""><strong>Mantenimiento</strong></label>
                    <p id=""mantenimiento"">Mantenimiento</p>
                </div>
                <div class=""form-group"">
                    <label for=""nomProcedimiento"" class=""control-label""><strong>Procedimiento</strong></label>
                    <p id=""procedimiento"">Procedimiento</p>
                </div>
                <div");
            WriteLiteral(@" class=""form-group"">
                    <label for=""empleadoID"" class=""control-label""><strong>Encargado</strong></label>
                    <p id=""empleadoID"">Encargado</p>
                </div>
                <div class=""form-group"">
                    <label for=""fechaSalida"" class=""control-label""><strong>Entrega</strong></label>
                    <p id=""fechaSalida"">Entrega</p>
                </div>
                <div class=""form-group"">
                    <label for=""estadoTarea"" class=""control-label""><strong>Estado</strong></label>
                    <p id=""estadoTarea"">Estado</p>
                </div>
                <input type=""hidden"" name=""asignacionTareaID"" id=""asignacionTareaID"">
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-dark"" data-dismiss=""modal"">Cancelar</button>
                <button type=""button"" class=""btn btn-danger"" onclick=""EliminarAsignacionTarea('AsignacionTareas/DeleteAsignacionTarea')"">E");
            WriteLiteral(@"liminar</button>
            </div>
        </div>
    </div>
</div>

<!----Modal Detalle-->
<div class=""modal fade"" id=""modalDetalleAsignacion"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4");
            BeginWriteAttribute("class", " class=\"", 6888, "\"", 6896, 0);
            EndWriteAttribute();
            WriteLiteral(@">Detalles de la asignación de tarea</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" arial-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
            <div class=""modal-body"">
                <div class=""form-group"">
                    <label for=""dAutomovilID"" class=""control-label""><strong>Automóvil</strong></label>
                    <p id=""dAutomovilID"">Placa automóvil</p>
                </div>
                <div class=""form-group"">
                    <label for=""dNomMantenimiento"" class=""control-label""><strong>Mantenimiento</strong></label>
                    <p id=""dMantenimiento"">Mantenimiento</p>
                </div>
                <div class=""form-group"">
                    <label for=""dNomProcedimiento"" class=""control-label""><strong>Procedimiento</strong></label>
                    <p id=""dProcedimiento"">Procedimiento</p>
                </div>
                <div class=");
            WriteLiteral(@"""form-group"">
                    <label for=""dEmpleadoID"" class=""control-label""><strong>Encargado</strong></label>
                    <p id=""dEmpleadoID"">Encargado</p>
                </div>
                <div class=""form-group"">
                    <label for=""dFechaSalida"" class=""control-label""><strong>Entrega</strong></label>
                    <p id=""dFechaSalida"">Entrega</p>
                </div>
                <div class=""form-group"">
                    <label for=""dEstadoTarea"" class=""control-label""><strong>Estado</strong></label>
                    <p id=""dEstadoTarea"">Estado</p>
                </div>
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Cerrar</button>
                <button type=""button"" class=""btn btn-success"" data-target=""#modalEditarAsignacion"" data-toggle=""modal"" onclick=""OcultarDetalleAsignacion()"">Editar</button>
            </div>
        </div>
    </div>
</di");
            WriteLiteral(@"v>

<!-- Modal Editar -->
<div class=""modal fade"" id=""modalEditarAsignacion"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"">
    <div class=""modal-dialog modal-dialog-scrollable"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""myModalLabel"">Editar estado de asignación de tarea</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
            </div>
            <div class=""modal-body"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c029327", async() => {
                WriteLiteral("\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c029606", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 200 "C:\Users\vancr\Escritorio\tallerHernan\TallerHernandez\Views\AsignacionTareas\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.ModelOnly;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                    <input type=""hidden"" name=""Id"" id=""Id"" />
                    <div class=""form-group"">
                        <label for=""eAutomovil"" class=""control-label""><strong>Automóvil</strong></label>
                        <input readonly name=""eAutomovil"" id=""eAutomovil"" class=""form-control"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""eMantenimiento"" class=""control-label""><strong>Mantenimiento</strong></label>
                        <input readonly name=""eMantenimiento"" id=""eMantenimiento"" class=""form-control"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""eProcedimiento"" class=""control-label""><strong>Procedimiento</strong></label>
                        <input readonly name=""eProcedimiento"" id=""eProcedimiento"" class=""form-control"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""eEmplead");
                WriteLiteral(@"o"" class=""control-label""><strong>Encargado</strong></label>
                        <input readonly name=""eEmpleado"" id=""eEmpleado"" class=""form-control"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""eFechaSalida"" class=""control-label""><strong>Entrega</strong></label>
                        <input readonly name=""eFechaSalida"" id=""eFechaSalida"" class=""form-control"" />
                    </div>
                    <div class=""form-group"">
                        <label for=""eEstadoTarea"" class=""control-label""><strong>Estado</strong></label>
                        <select name=""eEstadoTarea"" id=""eEstadoTarea"" class=""form-control"">
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c033162", async() => {
                    WriteLiteral("No Finalizada");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "90064188b7b2636f294dd7f21b24e2951e2659c034421", async() => {
                    WriteLiteral("Finalizada");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </select>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Cerrar</button>
                <button type=""button"" class=""btn btn-primary"" onclick=""EditarAsignacionTarea('AsignacionTareas/EditarAsignacionTarea')"">Guardar Cambios</button>
            </div>
        </div>
    </div>
</div>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<TallerHernandez.Models.AsignacionTarea>> Html { get; private set; }
    }
}
#pragma warning restore 1591
