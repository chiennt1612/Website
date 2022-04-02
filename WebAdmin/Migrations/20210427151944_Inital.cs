using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebAdmin.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaBox = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaRobotTag = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOnMenuMain = table.Column<bool>(type: "bit", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaBox = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaRobotTag = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuMainFooter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuMainFooter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuSubFooter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UrlAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuSubFooter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOnMenuMain = table.Column<bool>(type: "bit", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaBox = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaRobotTag = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsCategories_NewsCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "NewsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParamSetting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Since = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Hotline = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Order__Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "_Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Order__OrderStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "_OrderStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CategoryMain = table.Column<long>(type: "bigint", nullable: false),
                    CategoryReference = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSlide1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgSlide2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgSlide3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgSlide4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgSlide5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNews = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Quota = table.Column<int>(type: "int", nullable: false),
                    IsSale = table.Column<bool>(type: "bit", nullable: false),
                    IsHot = table.Column<bool>(type: "bit", nullable: false),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaBox = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaRobotTag = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Categories_CategoryMain",
                        column: x => x.CategoryMain,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryMain = table.Column<long>(type: "bigint", nullable: false),
                    CategoryReference = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    TagsReference = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Img = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImgBanner = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNews = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Publisher = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserCreator = table.Column<long>(type: "bigint", nullable: false),
                    DateCreator = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserModify = table.Column<long>(type: "bigint", nullable: true),
                    DateModify = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserDeleted = table.Column<long>(type: "bigint", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaBox = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    MetaRobotTag = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_NewsCategories_CategoryMain",
                        column: x => x.CategoryMain,
                        principalTable: "NewsCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "_OrderItem",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderItem", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK__OrderItem__Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__Order_AddressId",
                table: "_Order",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX__Order_StatusId",
                table: "_Order",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX__OrderItem_OrderId",
                table: "_OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_CategoryMain",
                table: "Article",
                column: "CategoryMain");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsCategories_ParentId",
                table: "NewsCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryMain",
                table: "Product",
                column: "CategoryMain");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_OrderItem");

            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "MenuMainFooter");

            migrationBuilder.DropTable(
                name: "MenuSubFooter");

            migrationBuilder.DropTable(
                name: "ParamSetting");

            migrationBuilder.DropTable(
                name: "_Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "NewsCategories");

            migrationBuilder.DropTable(
                name: "_Address");

            migrationBuilder.DropTable(
                name: "_OrderStatus");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
