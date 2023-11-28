using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_Management.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "FoodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "FoodId", "Category", "Description", "FoodName", "IsAvailable", "Picture", "Price" },
                values: new object[,]
                {
                    { 1, 1, "ABC1", "Product 1", true, "1.jpg", 1147.00m },
                    { 2, 3, "ABC2", "Product 2", true, "2.jpg", 1024.00m },
                    { 3, 1, "ABC3", "Product 3", true, "3.jpg", 1694.00m },
                    { 4, 1, "ABC4", "Product 4", true, "4.jpg", 1611.00m },
                    { 5, 3, "ABC5", "Product 5", true, "5.jpg", 1505.00m }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "Address", "CustomerName", "Email", "FoodId", "OrderDate", "Phone", "Quantity", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "Address 1", "Customer 1", "customer1@example.com", 1, new DateTime(2022, 7, 3, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(3932), 154592960, 119, 1720.00m },
                    { 2, "Address 2", "Customer 2", "customer2@example.com", 2, new DateTime(2022, 9, 20, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(3969), 537150046, 285, 1704.00m },
                    { 3, "Address 3", "Customer 3", "customer3@example.com", 3, new DateTime(2022, 8, 23, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(3983), 816122558, 241, 1951.00m },
                    { 4, "Address 4", "Customer 4", "customer4@example.com", 4, new DateTime(2022, 10, 11, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(3995), 266450887, 242, 1111.00m },
                    { 5, "Address 5", "Customer 5", "customer5@example.com", 5, new DateTime(2022, 9, 16, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(4008), 463968587, 203, 1058.00m },
                    { 6, "Address 6", "Customer 6", "customer6@example.com", 1, new DateTime(2022, 5, 27, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(4022), 852215886, 143, 1870.00m },
                    { 7, "Address 7", "Customer 7", "customer7@example.com", 2, new DateTime(2022, 7, 25, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(4035), 321986724, 266, 1913.00m },
                    { 8, "Address 8", "Customer 8", "customer8@example.com", 3, new DateTime(2022, 7, 20, 1, 51, 33, 646, DateTimeKind.Local).AddTicks(4058), 970684855, 246, 1095.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FoodId",
                table: "Orders",
                column: "FoodId");

            //Stored Procedures for Food table

            //InsertFood
            string sqlInsertFood = @"CREATE PROCEDURE InsertFood
                            @n VARCHAR(100),
                            @c INT,
                            @d VARCHAR(100),
                            @p MONEY,
                            @pic VARCHAR(100),
                            @ia BIT
                        AS
                        BEGIN
                            INSERT INTO Foods (FoodName, Category, Description, Price, Picture, IsAvailable)
                            VALUES (@n, @c, @d, @p, @pic, @ia)
                        END";
            migrationBuilder.Sql(sqlInsertFood);

            //UpdateFood
            string sqlUpdateFood = @"CREATE PROCEDURE UpdateFood
                                        @id INT,
                                        @n VARCHAR(100),
                                        @c INT,
                                        @d VARCHAR(100),
                                        @p MONEY,
                                        @pic VARCHAR(100),
                                        @ia BIT
                                    AS
                                    BEGIN
                                        UPDATE Foods
                                        SET FoodName = @n, Category = @c, Description = @d, Price = @p, Picture = @pic, IsAvailable = @ia
                                        WHERE FoodId = @id
                                    END";
            migrationBuilder.Sql(sqlUpdateFood);

            //DeleteFood
            string sqlDeleteFood = @"CREATE PROCEDURE DeleteFood
                                        @id INT
                                    AS
                                    BEGIN
                                        DELETE FROM Foods
                                        WHERE FoodId = @id
                                    END";
            migrationBuilder.Sql(sqlDeleteFood);

            //InsertOrder
            string sqlInsertOrder = @"CREATE PROCEDURE InsertOrder
                        @cn VARCHAR(100),
                        @phone INT,
                        @email VARCHAR(100),
                        @address VARCHAR(100),
                        @od DATETIME,
                        @qty INT,
                        @ta MONEY,
                        @fid INT
                    AS
                    BEGIN
                        INSERT INTO Orders (CustomerName, Phone, Email, Address, OrderDate, Quantity, TotalAmount, FoodId)
                        VALUES (@cn, @phone, @email, @address, @od, @qty, @ta, @fid)
                    END";
            migrationBuilder.Sql(sqlInsertOrder);

            //UpdateOrder
            string sqlUpdatetOrder = @"CREATE PROCEDURE UpdateOrder
                            @id INT,
                            @cn VARCHAR(100),
                            @phone INT,
                            @email VARCHAR(100),
                            @address VARCHAR(100),
                            @od DATETIME,
                            @qty INT,
                            @ta MONEY,
                            @fid INT
                        AS
                        BEGIN
                            UPDATE Orders
                            SET CustomerName = @cn, Phone = @phone, Email = @email, Address = @address,
                                OrderDate = @od, Quantity = @qty, TotalAmount = @ta, FoodId = @fid
                            WHERE OrderId = @id
                        END";
            migrationBuilder.Sql(sqlUpdatetOrder);

            //DeleteOrder
            string sqlDeleteOrder = @"CREATE PROCEDURE DeleteOrder 
                                        @id INT
                                    AS
                                    BEGIN
                                        DELETE FROM Orders WHERE OrderId = @id
                                    END";
            migrationBuilder.Sql(sqlDeleteOrder);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.Sql("DROP PROC InsertFood");
            migrationBuilder.Sql("DROP PROC UpdateFood");
            migrationBuilder.Sql("DROP PROC DeleteFood");

            migrationBuilder.Sql("DROP PROC InsertOrder");
            migrationBuilder.Sql("DROP PROC UpdateOrder");
            migrationBuilder.Sql("DROP PROC DeleteOrder");
        }
    }
}
