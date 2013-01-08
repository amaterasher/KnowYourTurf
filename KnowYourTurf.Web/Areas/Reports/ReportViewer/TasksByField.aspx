﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyPayments.aspx.cs" Inherits="KnowYourTurf.Web.Areas.Reports.ReportViewer.TasksByField" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
        <form id="form2" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReportViewer1" 
            runat="server" 
            Font-Names="Verdana" 
            Font-Size="8pt" 
            WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt"
            Width="875"
            Height="875">
            <LocalReport ReportPath="Areas\Reports\RDLC\TasksByField.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="TasksByField" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString='<%$ appSettings:MethodFitness.sql_server_connection_string %>'
        SelectCommand="TasksByField" SelectCommandType="StoredProcedure"> 
        <SelectParameters>
            <asp:QueryStringParameter Name="Date" DefaultValue="dbnull" QueryStringField="Date" Type="DateTime" />
        </SelectParameters>
    </asp:SqlDataSource>
    
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </form>
</body>
</html>
