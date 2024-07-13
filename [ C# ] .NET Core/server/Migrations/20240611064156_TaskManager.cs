using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class TaskManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROJECT",
                columns: table => new
                {
                    IDProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDLeader = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DayCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECT", x => x.IDProject);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE",
                columns: table => new
                {
                    IDTemplate = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TemplateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEMPLATE", x => x.IDTemplate);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    IDUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Avatar = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.IDUser);
                });

            migrationBuilder.CreateTable(
                name: "STATUS",
                columns: table => new
                {
                    IDStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatusOrder = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATUS", x => x.IDStatus);
                    table.ForeignKey(
                        name: "FK_STATUS_PROJECT_IDProject",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                });

            migrationBuilder.CreateTable(
                name: "LISTTEMPLATE",
                columns: table => new
                {
                    IDListTemplate = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDTemplate = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatusOrder = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LISTTEMPLATE", x => x.IDListTemplate);
                    table.ForeignKey(
                        name: "FK_LISTTEMPLATE_TEMPLATE_IDTemplate",
                        column: x => x.IDTemplate,
                        principalTable: "TEMPLATE",
                        principalColumn: "IDTemplate");
                });

            migrationBuilder.CreateTable(
                name: "MEMBER",
                columns: table => new
                {
                    IDMember = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEMBER", x => x.IDMember);
                    table.ForeignKey(
                        name: "FK_MEMBER_PROJECT_IDProject",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK_MEMBER_USER_IDUser",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "PERMISSION",
                columns: table => new
                {
                    IDPermission = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Object = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Privilege = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSION", x => x.IDPermission);
                    table.ForeignKey(
                        name: "FK_PERMISSION_PROJECT_IDProject",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK_PERMISSION_USER_IDUser",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    IDTask = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDProject = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DayStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK", x => x.IDTask);
                    table.ForeignKey(
                        name: "FK_TASK_PROJECT_IDProject",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK_TASK_STATUS_IDStatus",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "WORKFLOW",
                columns: table => new
                {
                    IDWorkflow = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Transition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WORKFLOW", x => x.IDWorkflow);
                    table.ForeignKey(
                        name: "FK_WORKFLOW_STATUS_IDStatus",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "ASSIGNMENT",
                columns: table => new
                {
                    IDUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDTask = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDAssignment = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "(newid())"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ASSIGNMENT", x => new { x.IDUser, x.IDTask });
                    table.ForeignKey(
                        name: "FK_ASSIGNMENT_TASK_IDTask",
                        column: x => x.IDTask,
                        principalTable: "TASK",
                        principalColumn: "IDTask");
                    table.ForeignKey(
                        name: "FK_ASSIGNMENT_USER_IDUser",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "TASKDETAILS",
                columns: table => new
                {
                    IDTask = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASKDETAILS", x => x.IDTask);
                    table.ForeignKey(
                        name: "FK_TASKDETAILS_TASK_IDTask",
                        column: x => x.IDTask,
                        principalTable: "TASK",
                        principalColumn: "IDTask");
                });

            migrationBuilder.CreateTable(
                name: "CONDITION",
                columns: table => new
                {
                    IDCondition = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    IDPermission = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDWorkflow = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONDITION", x => x.IDCondition);
                    table.ForeignKey(
                        name: "FK_CONDITION_PERMISSION_IDPermission",
                        column: x => x.IDPermission,
                        principalTable: "PERMISSION",
                        principalColumn: "IDPermission");
                    table.ForeignKey(
                        name: "FK_CONDITION_WORKFLOW_IDWorkflow",
                        column: x => x.IDWorkflow,
                        principalTable: "WORKFLOW",
                        principalColumn: "IDWorkflow");
                });

            migrationBuilder.InsertData(
                table: "TEMPLATE",
                columns: new[] { "IDTemplate", "IsDeleted", "TemplateName" },
                values: new object[,]
                {
                    { new Guid("7f5fa154-7421-4f9d-93f7-fbb8c6c33440"), false, "Design Management" },
                    { new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "IT Management" },
                    { new Guid("dec11f93-031a-422a-abfa-c80b226ec49b"), false, "Default" }
                });

            migrationBuilder.InsertData(
                table: "LISTTEMPLATE",
                columns: new[] { "IDListTemplate", "IDTemplate", "IsDeleted", "StatusName", "StatusOrder" },
                values: new object[,]
                {
                    { new Guid("12598434-2e65-4e66-a675-2d91c792ad8b"), new Guid("7f5fa154-7421-4f9d-93f7-fbb8c6c33440"), false, "Idea", 2 },
                    { new Guid("23b18ed4-4164-4994-b24c-9815d6326421"), new Guid("dec11f93-031a-422a-abfa-c80b226ec49b"), false, "To Do", 1 },
                    { new Guid("26fe8475-c033-4723-9cec-b25c6bc23f46"), new Guid("dec11f93-031a-422a-abfa-c80b226ec49b"), false, "In Progress", 2 },
                    { new Guid("328dac5f-b0b9-4bdf-b9a1-35a16e3dd479"), new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "In Preview", 3 },
                    { new Guid("3a2d4ea2-97ad-4146-ad20-8fa454ea7cce"), new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "In Progress", 2 },
                    { new Guid("3af50b83-ae2e-4fee-903c-c9405c941158"), new Guid("7f5fa154-7421-4f9d-93f7-fbb8c6c33440"), false, "Sketch", 3 },
                    { new Guid("506e9f1b-c15c-479e-803b-de07108057ac"), new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "Done", 6 },
                    { new Guid("7164fc40-1a72-4b34-a6d9-b7342151737b"), new Guid("7f5fa154-7421-4f9d-93f7-fbb8c6c33440"), false, "Done", 5 },
                    { new Guid("739a1cff-cd85-48db-99ba-b44d6b7f890a"), new Guid("dec11f93-031a-422a-abfa-c80b226ec49b"), false, "Done", 4 },
                    { new Guid("7da0e7ce-0294-4122-ab4a-b80260baea1b"), new Guid("7f5fa154-7421-4f9d-93f7-fbb8c6c33440"), false, "In Preview", 4 },
                    { new Guid("8e1545f1-acf4-4176-8c8b-b85f1446aba6"), new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "Test", 4 },
                    { new Guid("d010671d-22ad-4d15-85f0-49b814add9db"), new Guid("dec11f93-031a-422a-abfa-c80b226ec49b"), false, "In Preview", 3 },
                    { new Guid("f57ee129-81e7-4e7e-b93f-74a96238bc01"), new Guid("7f5fa154-7421-4f9d-93f7-fbb8c6c33440"), false, "To Do", 1 },
                    { new Guid("f8e4ceb8-0b88-4c69-a7f8-3ee5e245922f"), new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "Bug", 5 },
                    { new Guid("fccacfd4-02c0-49e4-8952-4d20eb605517"), new Guid("9a64a2e8-b37b-462f-bfde-0fdd88b2caec"), false, "To Do", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_IDTask",
                table: "ASSIGNMENT",
                column: "IDTask");

            migrationBuilder.CreateIndex(
                name: "IX_CONDITION_IDPermission",
                table: "CONDITION",
                column: "IDPermission");

            migrationBuilder.CreateIndex(
                name: "IX_CONDITION_IDWorkflow",
                table: "CONDITION",
                column: "IDWorkflow");

            migrationBuilder.CreateIndex(
                name: "IX_LISTTEMPLATE_IDTemplate",
                table: "LISTTEMPLATE",
                column: "IDTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_IDProject",
                table: "MEMBER",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_IDUser",
                table: "MEMBER",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSION_IDProject",
                table: "PERMISSION",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSION_IDUser",
                table: "PERMISSION",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_STATUS_IDProject",
                table: "STATUS",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_IDProject",
                table: "TASK",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_IDStatus",
                table: "TASK",
                column: "IDStatus");

            migrationBuilder.CreateIndex(
                name: "UNIQUE_TemplateName",
                table: "TEMPLATE",
                column: "TemplateName",
                unique: true,
                filter: "[TemplateName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WORKFLOW_IDStatus",
                table: "WORKFLOW",
                column: "IDStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASSIGNMENT");

            migrationBuilder.DropTable(
                name: "CONDITION");

            migrationBuilder.DropTable(
                name: "LISTTEMPLATE");

            migrationBuilder.DropTable(
                name: "MEMBER");

            migrationBuilder.DropTable(
                name: "TASKDETAILS");

            migrationBuilder.DropTable(
                name: "PERMISSION");

            migrationBuilder.DropTable(
                name: "WORKFLOW");

            migrationBuilder.DropTable(
                name: "TEMPLATE");

            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "STATUS");

            migrationBuilder.DropTable(
                name: "PROJECT");
        }
    }
}
