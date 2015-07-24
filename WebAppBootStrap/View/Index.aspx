<%@ Page Title="首页" Language="C#" MasterPageFile="~/Bootstrap.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebAppBS.View.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                服务器基本信息
            </div>
        </div>
        <div class="panel-body">
            <table class="table table-striped">
                <tr>
                    <td class="text-right">服务器计算机名</td>
                    <td class="text-left">
                        <asp:Label ID="lbServerName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器IP地址</td>
                    <td class="text-left">
                        <asp:Label ID="lbIp" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器域名</td>
                    <td class="text-left">
                        <asp:Label ID="lbDomain" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器端口</td>
                    <td class="text-left">
                        <asp:Label ID="lbPort" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器IIS版本</td>
                    <td class="text-left">
                        <asp:Label ID="lbIISVer" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">本文件所在文件夹</td>
                    <td class="text-left">
                        <asp:Label ID="lbPhPath" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器操作系统</td>
                    <td class="text-left">
                        <asp:Label ID="lbOperat" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">系统所在文件夹</td>
                    <td class="text-left">
                        <asp:Label ID="lbSystemPath" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器脚本超时时间</td>
                    <td class="text-left">
                        <asp:Label ID="lbTimeOut" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器的语言种类</td>
                    <td class="text-left">
                        <asp:Label ID="lbLan" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">.NET Framework 版本</td>
                    <td class="text-left">
                        <asp:Label ID="lbAspnetVer" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器当前时间</td>
                    <td class="text-left">
                        <asp:Label ID="lbCurrentTime" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器IE版本</td>
                    <td class="text-left">
                        <asp:Label ID="lbIEVer" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">服务器上次启动到现在已运行</td>
                    <td class="text-left">
                        <asp:Label ID="lbServerLastStartToNow" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">逻辑驱动器</td>
                    <td class="text-left">
                        <asp:Label ID="lbLogicDriver" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">CPU 总数</td>
                    <td class="text-left">
                        <asp:Label ID="lbCpuNum" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">CPU 类型</td>
                    <td class="text-left">
                        <asp:Label ID="lbCpuType" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">虚拟内存</td>
                    <td class="text-left">
                        <asp:Label ID="lbMemory" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">当前程序占用内存</td>
                    <td class="text-left">
                        <asp:Label ID="lbMemoryPro" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">Asp.net所占内存</td>
                    <td class="text-left">
                        <asp:Label ID="lbMemoryNet" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">Asp.net所占CPU</td>
                    <td class="text-left">
                        <asp:Label ID="lbCpuNet" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">当前Session数量</td>
                    <td class="text-left">
                        <asp:Label ID="lbSessionNum" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">当前SessionID</td>
                    <td class="text-left">
                        <asp:Label ID="lbSession" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">当前系统用户名</td>
                    <td class="text-left">
                        <asp:Label ID="lbUser" runat="server"></asp:Label></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
