namespace Snippy.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.SeedRoles(context);
            this.SeedUsers(context);
            this.SeedLanguages(context);
            this.SeedLables(context);
            this.SeedSnippets(context);
            this.SeedComments(context);
        }

        private void SeedComments(ApplicationDbContext context)
        {
            if (!context.Comments.Any())
            {
                var admin = context.Users.FirstOrDefault(u => u.UserName == "Admin");
                var newUser = context.Users.FirstOrDefault(u => u.UserName == "newUser");
                var someUser = context.Users.FirstOrDefault(u => u.UserName == "someUser");

                var ternaryMadnes = context.Snippets.FirstOrDefault(s => s.Title == "Ternary Operator Madness");
                var reversedString = context.Snippets.FirstOrDefault(s => s.Title == "Reverse a String");
                var circle = context.Snippets.FirstOrDefault(s => s.Title == "Points Around A Circle For GameObject Placement");
                var inputField = context.Snippets.FirstOrDefault(s => s.Title == "Numbers only in an input field");

                var comments = new[]
                {
                    new Comment
                    {
                        Content = "Now that's really funny! I like it!",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 10, 30, 11, 50, 38),
                        Snippet = ternaryMadnes
                    },
                    new Comment
                    {
                        Content = "Here, have my comment!",
                        Author = newUser,
                        CreatedOn = new DateTime(2018, 11, 01, 15, 52, 42),
                        Snippet = ternaryMadnes
                    },
                    new Comment
                    {
                        Content = "I didn't manage to comment first :(",
                        Author = someUser,
                        CreatedOn = new DateTime(2018, 11, 02, 05, 30, 20),
                        Snippet = ternaryMadnes
                    },
                    new Comment
                    {
                        Content = "That's why I love Python - everything is so simple!",
                        Author = newUser,
                        CreatedOn = new DateTime(2018, 10, 27, 15, 28, 14),
                        Snippet = reversedString
                    },
                    new Comment
                    {
                        Content =
                            "I have always had problems with Geometry in school. Thanks to you I can now do this without ever having to learn this damn subject",
                        Author = someUser,
                        CreatedOn = new DateTime(2018, 10, 29, 15, 08, 42),
                        Snippet = circle
                    },
                    new Comment
                    {
                        Content =
                            "Thank you. However, I think there must be a simpler way to do this. I can't figure it out now, but I'll post it when I'm ready.",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 11, 03, 12, 56, 20),
                        Snippet = inputField
                    }
                };

                foreach (var comment in comments)
                {
                    context.Comments.Add(comment);
                }

                context.SaveChanges();
            }
        }

        private void SeedLables(ApplicationDbContext context)
        {
            if (!context.Labels.Any())
            {
                var labels = new[]
                {
                    new Label {Text = "bug"},
                    new Label {Text = "funny"},
                    new Label {Text = "jquery"},
                    new Label {Text = "mysql"},
                    new Label {Text = "useful"},
                    new Label {Text = "web"},
                    new Label {Text = "geometry"},
                    new Label {Text = "back-end"},
                    new Label {Text = "front-end"},
                    new Label {Text = "games"},
                };

                foreach (var label in labels)
                {
                    context.Labels.Add(label);
                }

                context.SaveChanges();
            }
        }

        private void SeedLanguages(ApplicationDbContext context)
        {
            if (!context.Languages.Any())
            {
                var languages = new[]
                {
                    new Language {Name = "C#"},
                    new Language {Name = "JavaScript"},
                    new Language {Name = "Python"},
                    new Language {Name = "CSS"},
                    new Language {Name = "SQL"},
                    new Language {Name = "PHP"},
                };

                foreach (var language in languages)
                {
                    context.Languages.Add(language);
                }

                context.SaveChanges();
            }
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }
        }


        private void SeedUsers(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                this.AddAdmin(userManager);

                this.AddUser(userManager, "someUser", "someUser@example.com", "someUserPass123");
                this.AddUser(userManager, "newUser", "new_user@gmail.com", "userPass123");
            }
        }

        private void AddUser(UserManager<ApplicationUser> userManager, string username, string email, string password)
        {
            var someUser = new ApplicationUser
            {
                UserName = username,
                Email = email,
            };

            var userCreateResult = userManager.Create(someUser, password);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }
        }

        private void AddAdmin(UserManager<ApplicationUser> userManager)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@snippy.com",
            };

            string adminPassword = "adminPass123";
            var adminCreateResult = userManager.Create(admin, adminPassword);
            if (!adminCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", adminCreateResult.Errors));
            }

            string adminRole = "Admin";
            var addToRoleResult = userManager.AddToRole(admin.Id, adminRole);
            if (!addToRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addToRoleResult.Errors));
            }
        }

        private void SeedSnippets(ApplicationDbContext context)
        {
            if (!context.Snippets.Any())
            {
                var admin = context.Users.FirstOrDefault(u => u.UserName == "Admin");
                var newUser = context.Users.FirstOrDefault(u => u.UserName == "newUser");
                var someUser = context.Users.FirstOrDefault(u => u.UserName == "someUser");

                var csharp = context.Languages.FirstOrDefault(l => l.Name == "C#");
                var javaScript = context.Languages.FirstOrDefault(l => l.Name == "JavaScript");
                var sql = context.Languages.FirstOrDefault(l => l.Name == "SQL");
                var python = context.Languages.FirstOrDefault(l => l.Name == "Python");
                var css = context.Languages.FirstOrDefault(l => l.Name == "CSS");

                var labelFunny = context.Labels.FirstOrDefault(l => l.Text == "funny");
                var labelGeometry = context.Labels.FirstOrDefault(l => l.Text == "geometry");
                var labelGames = context.Labels.FirstOrDefault(l => l.Text == "games");
                var labelJQuery = context.Labels.FirstOrDefault(l => l.Text == "jquery");
                var labelUseful = context.Labels.FirstOrDefault(l => l.Text == "useful");
                var labelWeb = context.Labels.FirstOrDefault(l => l.Text == "web");
                var labelFontEnd = context.Labels.FirstOrDefault(l => l.Text == "front-end");
                var labelBug = context.Labels.FirstOrDefault(l => l.Text == "bug");
                var labelMySql = context.Labels.FirstOrDefault(l => l.Text == "mysql");

                var snippets = new[]
                {
                    new Snippet
                    {
                        Title = "Ternary Operator Madness", 
                        Description = "This is how you DO NOT user ternary operators in C#!",
                        Code = "bool X = Glob.UserIsAdmin ? true : false;",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 10, 26, 17, 20, 33),
                        Language = csharp,
                        Labels = new List<Label> { labelFunny }
                    },
                    new Snippet
                    {
                        Title = "Points Around A Circle For GameObject Placement", 
                        Description = "Determine points around a circle which can be used to place elements around a central point",
                        Code = @"private Vector3 DrawCircle(float centerX, float centerY, float radius, float totalPoints, float currentPoint)
{
	float ptRatio = currentPoint / totalPoints;
	float pointX = centerX + (Mathf.Cos(ptRatio * 2 * Mathf.PI)) * radius;
	float pointY = centerY + (Mathf.Sin(ptRatio * 2 * Mathf.PI)) * radius;

	Vector3 panelCenter = new Vector3(pointX, pointY, 0.0f);

	return panelCenter;
}
",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 10, 26, 20, 15, 30),
                        Language = csharp,
                        Labels = new List<Label> { labelGeometry, labelGames }
                    },
                    new Snippet
                    {
                        Title = "forEach. How to break?", 
                        Description = "Array.prototype.forEach You can't break forEach. So use \"some\" or \"every\". Array.prototype.some some is pretty much the same as forEach but it break when the callback returns true. Array.prototype.every every is almost identical to some except it's expecting false to break the loop.",
                        Code = @"var ary = [""JavaScript"", ""Java"", ""CoffeeScript"", ""TypeScript""];
 
ary.some(function (value, index, _ary) {
	console.log(index + "": "" + value);
	return value === ""CoffeeScript"";
});
// output: 
// 0: JavaScript
// 1: Java
// 2: CoffeeScript
 
ary.every(function(value, index, _ary) {
	console.log(index + "": "" + value);
	return value.indexOf(""Script"") > -1;
});
// output:
// 0: JavaScript
// 1: Java
",
                        Author = newUser,
                        CreatedOn = new DateTime(2018, 10, 27, 10, 53, 20),
                        Language = javaScript,
                        Labels = new List<Label> { labelJQuery, labelUseful, labelWeb, labelFontEnd }
                    },
                    new Snippet
                    {
                        Title = "Numbers only in an input field", 
                        Description = "Method allowing only numbers (positive / negative / with commas or decimal points) in a field",
                        Code = @"$(""#input"").keypress(function(event){
	var charCode = (event.which) ? event.which : window.event.keyCode;
	if (charCode <= 13) { return true; } 
	else {
		var keyChar = String.fromCharCode(charCode);
		var regex = new RegExp(""[0-9,.-]"");
		return regex.test(keyChar); 
	} 
});
",
                        Author = someUser,
                        CreatedOn = new DateTime(2018, 10, 28, 09, 00, 56),
                        Language = javaScript,
                        Labels = new List<Label> { labelWeb, labelFontEnd }
                    },
                    new Snippet
                    {
                        Title = "Create a link directly in an SQL query", 
                        Description = "That's how you create links - directly in the SQL!",
                        Code = @"SELECT DISTINCT
              b.Id,
              concat('<button type=""button"" onclick=""DeleteContact(', cast(b.Id as char), ')"">Delete...</button>') as lnkDelete
FROM tblContact   b
WHERE ....
",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 10, 30, 11, 20, 00),
                        Language = sql,
                        Labels = new List<Label> { labelBug, labelFunny, labelMySql }
                    },
                     new Snippet
                    {
                        Title = "Reverse a String", 
                        Description = "Almost not worth having a function for...",
                        Code = @"def reverseString(s):
	""""""Reverses a string given to it.""""""
	return s[::-1]
",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 10, 26, 09, 35, 13),
                        Language = python,
                        Labels = new List<Label> { labelUseful }
                    },
                    new Snippet
                    {
                        Title = "Pure CSS Text Gradients", 
                        Description = "This code describes how to create text gradients using only pure CSS",
                        Code = @"/* CSS text gradients */
h2[data-text] {
	position: relative;
}
h2[data-text]::after {
	content: attr(data-text);
	z-index: 10;
	color: #e3e3e3;
	position: absolute;
	top: 0;
	left: 0;
	-webkit-mask-image: -webkit-gradient(linear, left top, left bottom, from(rgba(0,0,0,0)), color-stop(50%, rgba(0,0,0,1)), to(rgba(0,0,0,0)));
",
                        Author = someUser,
                        CreatedOn = new DateTime(2018, 10, 26, 19, 26, 42),
                        Language = css,
                        Labels = new List<Label> { labelWeb, labelFontEnd }
                    },
                    new Snippet
                    {
                        Title = "Check for a Boolean value in JS", 
                        Description = "How to check a Boolean value - the wrong but funny way :D",
                        Code = @"var b = true;

if (b.toString().length < 5) {
  //...
}
",
                        Author = admin,
                        CreatedOn = new DateTime(2018, 10, 22, 05, 30, 04),
                        Language = javaScript,
                        Labels = new List<Label> { labelFunny }
                    },
                };

                foreach (var snippet in snippets)
                {
                    context.Snippets.Add(snippet);
                }

                context.SaveChanges();
            }
        }
    }
}
