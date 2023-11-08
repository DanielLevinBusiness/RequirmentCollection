using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpsRequirmentCollectionWeb.Migrations
{
    /// <inheritdoc />
    public partial class DataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetItemsENG",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoradName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAndTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOther = table.Column<int>(type: "int", nullable: false),
                    ItemJustification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiStep = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemsENG", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetItemsENGApp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoradName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAndTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOther = table.Column<int>(type: "int", nullable: false),
                    ItemJustification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiStep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedQuantity = table.Column<int>(type: "int", nullable: true),
                    ApprovedQuantityOther = table.Column<int>(type: "int", nullable: true),
                    APO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemsENGApp", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetItemsPRO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAndTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOther = table.Column<int>(type: "int", nullable: false),
                    ItemJustification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skew = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemsPRO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetItemsPROApp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAndTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOther = table.Column<int>(type: "int", nullable: false),
                    ItemJustification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skew = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedQuantity = table.Column<int>(type: "int", nullable: true),
                    ApprovedQuantityOther = table.Column<int>(type: "int", nullable: true),
                    APO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemsPROApp", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetItemsSI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAndTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOther = table.Column<int>(type: "int", nullable: false),
                    ItemJustification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlindOrTested = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiFLV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiniSkew = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Step = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ETDDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemsSI", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetItemsSIApp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Requestor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupAndTeam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QuantityOther = table.Column<int>(type: "int", nullable: false),
                    ItemJustification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlindOrTested = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiFLV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiniSkew = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Step = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedQuantity = table.Column<int>(type: "int", nullable: true),
                    ApprovedQuantityOther = table.Column<int>(type: "int", nullable: true),
                    APO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ETA = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetItemsSIApp", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EngBoards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BoardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Setup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerUnit = table.Column<float>(type: "real", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EngBoards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GroupAndTeams",
                columns: table => new
                {
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ProductBoards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skew = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerUnit = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBoards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Requestors",
                columns: table => new
                {
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SiPackage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackageNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerUnit = table.Column<float>(type: "real", nullable: false),
                    SiFLV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiPackage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "submitStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submitStatus", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetItemsENG");

            migrationBuilder.DropTable(
                name: "BudgetItemsENGApp");

            migrationBuilder.DropTable(
                name: "BudgetItemsPRO");

            migrationBuilder.DropTable(
                name: "BudgetItemsPROApp");

            migrationBuilder.DropTable(
                name: "BudgetItemsSI");

            migrationBuilder.DropTable(
                name: "BudgetItemsSIApp");

            migrationBuilder.DropTable(
                name: "EngBoards");

            migrationBuilder.DropTable(
                name: "GroupAndTeams");

            migrationBuilder.DropTable(
                name: "ProductBoards");

            migrationBuilder.DropTable(
                name: "Requestors");

            migrationBuilder.DropTable(
                name: "SiPackage");

            migrationBuilder.DropTable(
                name: "submitStatus");
        }
    }
}
