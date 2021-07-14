<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="electionlist.aspx.cs" Inherits="E_Polling_Web_Application.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-8 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Election List</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:epollingDBConnectionString %>" SelectCommand="SELECT * FROM [election_master_table]"></asp:SqlDataSource>
                            <div class="col">
                                <div class="col">
                                    <asp:GridView class="table table-striped table-bordered table-responsive-md" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="election_id" DataSourceID="SqlDataSource1">
                                        <Columns>
                                            <asp:BoundField DataField="election_id" HeaderText="Election ID" SortExpression="election_id" />
                                            <asp:BoundField DataField="election_name" HeaderText="Election Name" SortExpression="election_name" />
                                            <asp:HyperLinkField DataNavigateUrlFields="election_link" HeaderText="Election Link" SortExpression="election_link" DataTextFormatString="Click Here" DataTextField="election_link" />
                                            <asp:HyperLinkField DataNavigateUrlFields="results_link" HeaderText="Results Link" SortExpression="results_link" DataTextFormatString="Click Here" DataTextField="results_link" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <a href="homepage.aspx"><< Back to Home</a><span class="clearfix"></span>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
