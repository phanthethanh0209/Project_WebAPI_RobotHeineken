using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheThanh_WebAPI_RobotHeineken.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                });

            migrationBuilder.CreateTable(
                name: "RobotType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RobotType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "RecyclingMachine",
                columns: table => new
                {
                    MachineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineCode = table.Column<int>(type: "int", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ContainerStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalInteractions = table.Column<int>(type: "int", nullable: false),
                    HeinekenCount = table.Column<int>(type: "int", nullable: false),
                    OtherCount = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LocationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecyclingMachine", x => x.MachineID);
                    table.ForeignKey(
                        name: "FK_RecyclingMachine_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Robot",
                columns: table => new
                {
                    RobotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RobotName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BatteryLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 100),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    TypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Robot", x => x.RobotID);
                    table.ForeignKey(
                        name: "FK_Robot_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Robot_RobotType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "RobotType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_LocationName",
                table: "Location",
                column: "LocationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecyclingMachine_LocationID",
                table: "RecyclingMachine",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_RecyclingMachine_MachineCode",
                table: "RecyclingMachine",
                column: "MachineCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Robot_LocationID",
                table: "Robot",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Robot_TypeID",
                table: "Robot",
                column: "TypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecyclingMachine");

            migrationBuilder.DropTable(
                name: "Robot");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "RobotType");
        }
    }
}
