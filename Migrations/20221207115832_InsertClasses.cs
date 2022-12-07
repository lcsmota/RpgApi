using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class InsertClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Warrior', 'The most powerful character. He has the best weapons and armor, high health score, attack, and defense.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Assassin', 'This character class has a more subtle approach, unlike warrior’s brute force. They use specific skills, such as stealing, to complete missions. Their abilities revolve around smaller and faster weapons.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Wizard', 'This character do not usually have traditional weapons but use spells to fight or defend themselves from attacks. They usually have the weakest armor because their strong point is fighting from a distance by throwing spells.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Archer', 'This character most often use a bow and arrow as weapons.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Berserker', 'This character class is represented by some monsters that use devastating blows which are designed to proceed quickly and do catastrophic damage.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Priest', 'Subclass of the Wizard, they help a lot in the diversity of the characters in the game. Most specialize in buffing, de-buffing, cleansing, and crowd control.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Necromancer', 'This character in game spread diseases such as plague and supplement the burst damage in the team.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Summoner', 'This character can do multiple damages simultaneously.');");

            mb.Sql(@"INSERT INTO RpgClasses(Name, Description) 
                    VALUES('Lancer', 'This class of forceful characters specializes in pole-type arms.');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM RpgClasses");
        }
    }
}
