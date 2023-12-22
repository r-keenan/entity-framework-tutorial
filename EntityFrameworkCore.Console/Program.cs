using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

// Create instance of context
using var context = new FootballLeagueDbContext();


//GetAllTeams();

// Getting the first one in the list
var teamOne = await context.Teams.FirstAsync();

// Select record that meets a condition or throws error
var teamTwo = await context.Teams.FirstAsync(t => t.Id == 2);
// returns a record or null
var teamTwoOrNull = await context.Teams.FirstOrDefaultAsync(t => t.Id == 2);
//Selecting a single record - Only one record should be returned or returns error if there is more than one record that can be returned
var teamThreeA = await context.Teams.SingleAsync(t => t.Id == 3);
//Selecting a single record - Only one record should be returned or returns null
var teamThreeB = await context.Teams.SingleOrDefaultAsync(t => t.Id == 3);
// Selecting based on Id
var teamBasedOnId = await context.Teams.FindAsync(5);
if (teamBasedOnId != null) Console.WriteLine(teamBasedOnId.Name);

// Select all teams
void GetAllTeams()
{
    var teams = context.Teams.ToList();

    foreach (var team in teams) Console.WriteLine(team.Name);
}