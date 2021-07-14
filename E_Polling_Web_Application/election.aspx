<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="election.aspx.cs" Inherits="E_Polling_Web_Application.WebForm14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-10 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <h4>Election Portal</h4>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr>
                            </div>
                        </div>
                        <div class="row">
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:epollingDBConnectionString %>" SelectCommand="SELECT * FROM [candidate_master_table]"></asp:SqlDataSource>
                            <div class="col">
                                <asp:GridView class="table table-bordered table-responsive-md table-hover" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="candidate_id" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:BoundField DataField="candidate_id" HeaderText="ID" ReadOnly="True" SortExpression="candidate_id">
                                            <ControlStyle Font-Bold="True" />
                                            <ItemStyle Font-Bold="True" Font-Size="Larger" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Candidate Profile" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-lg-4">
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("candidate_name") %>' Font-Size="Larger" Font-Bold="True"></asp:Label>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:Image class="img-fluid" ID="Image1" runat="server" ImageUrl='<%# Eval("candidate_img_link") %>' Height="100px" Width="100px" />
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <asp:Image class="img-fluid" ID="Image2" runat="server" ImageUrl='<%# Eval("symbol_img_link") %>' Height="100px" Width="100px" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" ShowHeader="True" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" HeaderText="Press To Vote" ButtonType="Image" SelectImageUrl="imgs\vote.png" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <a href="homepage.aspx"><< Back to Home</a><span class="clearfix"></span>
                <br>
            </div>
        </div>
    </div>
</asp:Content>
