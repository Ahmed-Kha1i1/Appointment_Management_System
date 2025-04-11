using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false, comment: "0-Male, 1-Female")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    GuestEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    AppointmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "0-Pending, 1-Confirmed, 2-Completed, 3-Cancelled")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Doctor" },
                    { 3, "Patient" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Paediatrician" },
                    { 2, "Internal Medicine Physician" },
                    { 3, "Gynaecologist" },
                    { 4, "Cardiologist" },
                    { 5, "Dermatologist" },
                    { 6, "Neurologist" },
                    { 7, "Psychiatrist" },
                    { 8, "Gastroenterologist" },
                    { 9, "Ophthalmologist" },
                    { 10, "Pulmonologist" },
                    { 11, "Nephrologist" },
                    { 12, "Dentist" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "PasswordHash" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "admin1@gmail.com", "Ahmed", "Magdy", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 2, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "admin2@gmail.com", "Mosa", "Ali", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 3, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "johndoe1@gmail.com", "John", "Doe", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 4, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "janesmith1@gmail.com", "Jane", "Smith", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 5, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "alicejohnson@gmail.com", "Alice", "Johnson", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 6, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "bobbrown@gmail.com", "Bob", "Brown", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 7, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "caroldavis@gmail.com", "Carol", "Davis", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 8, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "davidwilson@gmail.com", "David", "Wilson", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 9, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "evemartinez@gmail.com", "Eve", "Martinez", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 10, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "frankgarcia@gmail.com", "Frank", "Garcia", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 11, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "gracerodriguez@gmail.com", "Grace", "Rodriguez", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 12, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "hanklee@gmail.com", "Hank", "Lee", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 13, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "ivywalker@gmail.com", "Ivy", "Walker", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 14, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "jackhall@gmail.com", "Jack", "Hall", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 15, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "kathyallen@gmail.com", "Kathy", "Allen", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 16, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "leoyoung@gmail.com", "Leo", "Young", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 17, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "miahernandez@gmail.com", "Mia", "Hernandez", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 18, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "ninaking@gmail.com", "Nina", "King", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 19, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "oscarwright@gmail.com", "Oscar", "Wright", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 20, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "paullopez@gmail.com", "Paul", "Lopez", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 21, new DateTime(2025, 4, 7, 3, 10, 0, 0, DateTimeKind.Unspecified), "patient@gmail.com", "Ahmed", "Magdy", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 25, new DateTime(2025, 4, 10, 5, 20, 0, 0, DateTimeKind.Unspecified), "john.smith@example.com", "John", "Smith", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 27, new DateTime(2025, 4, 10, 5, 20, 0, 0, DateTimeKind.Unspecified), "michael.w@example.com", "Michael", "Williams", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 28, new DateTime(2025, 4, 10, 5, 20, 0, 0, DateTimeKind.Unspecified), "sarah.b@example.com", "Sarah", "Brown", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 29, new DateTime(2025, 4, 10, 5, 20, 0, 0, DateTimeKind.Unspecified), "davidz.m@example.com", "David", "Millerz", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 30, new DateTime(2025, 4, 10, 8, 22, 0, 0, DateTimeKind.Unspecified), "am234@gmail.com", "Ahmed", "Ali2", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" },
                    { 32, new DateTime(2025, 4, 10, 17, 2, 0, 0, DateTimeKind.Unspecified), "p2@gmail.com", "Davidz", "Ali", "ecf05f2b3a014fe7a5c2c689ffb454cfeccad80d1502138e6cabdd831a031710" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "SpecializationId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 2 },
                    { 6, 3 },
                    { 7, 3 },
                    { 8, 3 },
                    { 9, 4 },
                    { 10, 5 },
                    { 11, 5 },
                    { 12, 6 },
                    { 13, 7 },
                    { 14, 8 },
                    { 15, 8 },
                    { 16, 9 },
                    { 17, 10 },
                    { 18, 11 },
                    { 19, 11 },
                    { 20, 12 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "BirthDate", "Gender" },
                values: new object[,]
                {
                    { 21, new DateOnly(2001, 10, 25), 0 },
                    { 25, new DateOnly(1985, 7, 15), 0 },
                    { 27, new DateOnly(1978, 11, 5), 0 },
                    { 28, new DateOnly(1995, 9, 18), 1 },
                    { 29, new DateOnly(2007, 4, 10), 0 },
                    { 30, new DateOnly(2007, 4, 10), 1 },
                    { 32, new DateOnly(2007, 4, 10), 0 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 2, 3 },
                    { 4, 2, 4 },
                    { 5, 2, 5 },
                    { 6, 2, 6 },
                    { 7, 2, 7 },
                    { 8, 2, 8 },
                    { 9, 2, 9 },
                    { 10, 2, 10 },
                    { 11, 2, 11 },
                    { 12, 2, 12 },
                    { 13, 2, 13 },
                    { 14, 2, 14 },
                    { 15, 2, 15 },
                    { 16, 2, 16 },
                    { 17, 2, 17 },
                    { 18, 2, 18 },
                    { 19, 2, 19 },
                    { 20, 2, 20 },
                    { 21, 3, 21 },
                    { 25, 3, 21 },
                    { 27, 3, 21 },
                    { 28, 3, 21 },
                    { 29, 3, 21 },
                    { 30, 3, 21 },
                    { 32, 3, 21 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "CreatedDate", "DoctorId", "EndTime", "GuestEmail", "PatientId", "StartTime", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 1, 5), new DateTime(2023, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeOnly(10, 0, 0), null, 21, new TimeOnly(9, 0, 0), 2 },
                    { 2, new DateOnly(2025, 1, 10), new DateTime(2023, 12, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeOnly(15, 0, 0), null, 21, new TimeOnly(14, 0, 0), 2 },
                    { 3, new DateOnly(2025, 1, 15), new DateTime(2023, 12, 17, 11, 0, 0, 0, DateTimeKind.Unspecified), 5, new TimeOnly(11, 0, 0), null, 21, new TimeOnly(10, 0, 0), 2 },
                    { 4, new DateOnly(2025, 1, 20), new DateTime(2023, 12, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), 6, new TimeOnly(14, 0, 0), null, 21, new TimeOnly(13, 0, 0), 0 },
                    { 5, new DateOnly(2025, 2, 2), new DateTime(2025, 1, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, new TimeOnly(9, 0, 0), null, 21, new TimeOnly(8, 0, 0), 2 },
                    { 6, new DateOnly(2025, 2, 7), new DateTime(2025, 1, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 8, new TimeOnly(12, 0, 0), null, 21, new TimeOnly(11, 0, 0), 2 },
                    { 7, new DateOnly(2025, 5, 14), new DateTime(2025, 1, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 9, new TimeOnly(15, 0, 0), null, 21, new TimeOnly(14, 0, 0), 2 },
                    { 8, new DateOnly(2025, 2, 21), new DateTime(2025, 1, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, new TimeOnly(10, 0, 0), null, 21, new TimeOnly(9, 0, 0), 0 },
                    { 9, new DateOnly(2025, 3, 1), new DateTime(2025, 2, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 11, new TimeOnly(11, 0, 0), null, 21, new TimeOnly(10, 0, 0), 2 },
                    { 10, new DateOnly(2025, 3, 8), new DateTime(2025, 2, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), 12, new TimeOnly(15, 0, 0), null, 21, new TimeOnly(14, 0, 0), 2 },
                    { 11, new DateOnly(2025, 3, 15), new DateTime(2025, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 13, new TimeOnly(9, 0, 0), null, 21, new TimeOnly(8, 0, 0), 2 },
                    { 12, new DateOnly(2025, 3, 22), new DateTime(2025, 2, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 14, new TimeOnly(13, 0, 0), null, 21, new TimeOnly(12, 0, 0), 0 },
                    { 15, new DateOnly(2025, 4, 15), new DateTime(2025, 3, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 17, new TimeOnly(13, 0, 0), null, 21, new TimeOnly(12, 0, 0), 3 },
                    { 16, new DateOnly(2025, 4, 20), new DateTime(2025, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 18, new TimeOnly(17, 0, 0), null, 21, new TimeOnly(16, 0, 0), 0 },
                    { 17, new DateOnly(2025, 5, 3), new DateTime(2025, 4, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 18, new TimeOnly(11, 0, 0), null, 30, new TimeOnly(10, 0, 0), 1 },
                    { 18, new DateOnly(2025, 5, 10), new DateTime(2025, 4, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), 20, new TimeOnly(15, 0, 0), null, 21, new TimeOnly(14, 0, 0), 1 },
                    { 19, new DateOnly(2025, 5, 20), new DateTime(2025, 4, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 3, new TimeOnly(9, 0, 0), null, 21, new TimeOnly(8, 0, 0), 1 },
                    { 20, new DateOnly(2025, 5, 24), new DateTime(2025, 4, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeOnly(13, 0, 0), null, 21, new TimeOnly(12, 0, 0), 0 },
                    { 21, new DateOnly(2025, 4, 20), new DateTime(2025, 3, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 18, new TimeOnly(18, 0, 0), null, 21, new TimeOnly(17, 0, 0), 0 },
                    { 22, new DateOnly(2025, 5, 24), new DateTime(2025, 4, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeOnly(18, 0, 0), null, 21, new TimeOnly(17, 0, 0), 0 },
                    { 23, new DateOnly(2025, 5, 24), new DateTime(2025, 4, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, new TimeOnly(21, 0, 0), null, 21, new TimeOnly(20, 0, 0), 1 },
                    { 24, new DateOnly(2025, 5, 10), new DateTime(2025, 4, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), 20, new TimeOnly(16, 0, 0), null, 21, new TimeOnly(15, 0, 0), 3 },
                    { 25, new DateOnly(2025, 4, 13), new DateTime(2025, 4, 9, 22, 31, 0, 0, DateTimeKind.Unspecified), 11, new TimeOnly(21, 0, 0), null, 29, new TimeOnly(20, 0, 0), 0 },
                    { 26, new DateOnly(2025, 4, 12), new DateTime(2025, 4, 9, 22, 33, 0, 0, DateTimeKind.Unspecified), 6, new TimeOnly(23, 0, 0), null, 21, new TimeOnly(22, 0, 0), 0 },
                    { 28, new DateOnly(2025, 4, 13), new DateTime(2025, 4, 10, 0, 54, 0, 0, DateTimeKind.Unspecified), 9, new TimeOnly(7, 0, 0), null, 30, new TimeOnly(6, 0, 0), 0 },
                    { 29, new DateOnly(2025, 4, 12), new DateTime(2025, 4, 10, 0, 55, 0, 0, DateTimeKind.Unspecified), 4, new TimeOnly(6, 0, 0), null, 21, new TimeOnly(5, 0, 0), 1 },
                    { 30, new DateOnly(2025, 4, 11), new DateTime(2025, 4, 10, 11, 54, 0, 0, DateTimeKind.Unspecified), 12, new TimeOnly(15, 0, 0), null, 21, new TimeOnly(14, 0, 0), 0 },
                    { 31, new DateOnly(2025, 4, 13), new DateTime(2025, 4, 10, 14, 40, 0, 0, DateTimeKind.Unspecified), 4, new TimeOnly(16, 0, 0), "amdsf@gmai.lcom", null, new TimeOnly(15, 0, 0), 1 },
                    { 32, new DateOnly(2025, 4, 12), new DateTime(2025, 4, 10, 14, 41, 0, 0, DateTimeKind.Unspecified), 3, new TimeOnly(16, 0, 0), "Ahmed123@gmail.com", null, new TimeOnly(15, 0, 0), 0 },
                    { 33, new DateOnly(2025, 4, 11), new DateTime(2025, 4, 10, 17, 52, 0, 0, DateTimeKind.Unspecified), 3, new TimeOnly(13, 0, 0), "amdsf@gmai.lcom", null, new TimeOnly(12, 0, 0), 0 },
                    { 34, new DateOnly(2025, 4, 13), new DateTime(2025, 4, 10, 19, 4, 0, 0, DateTimeKind.Unspecified), 3, new TimeOnly(13, 0, 0), null, 28, new TimeOnly(12, 0, 0), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_Date",
                table: "Appointments",
                column: "AppointmentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_userId",
                table: "RefreshTokens",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
