public partial class CreateIdentitySchema : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Identity tables: AspNetUsers, AspNetRoles, AspNetUserRoles, etc.
        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(nullable: false),
                UserName = table.Column<string>(maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                Email = table.Column<string>(maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                PasswordHash = table.Column<string>(nullable: true),
                SecurityStamp = table.Column<string>(nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("AspNetUsers");
    }
}
