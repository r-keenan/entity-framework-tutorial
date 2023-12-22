using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

// Create instance of context
using var context = new FootballLeagueDbContext();


//await GetAllTeams();
//await GetOneTeam();
await GetFilteredTeams();


async Task GetFilteredTeams()
{
    Console.WriteLine("Enter Desired Team");
    var desiredTeam = Console.ReadLine();
    //Select all records that meet a condition
    var teamsFiltered = await context.Teams.Where(q => q.Name == desiredTeam).ToListAsync();
    foreach (var team in teamsFiltered) Console.WriteLine(team.Name);
}

// Select all teams
async Task GetAllTeams()
{
    var teams = await context.Teams.ToListAsync();

    foreach (var team in teams) Console.WriteLine(team.Name);
}

// Get one team
async Task GetOneTeam()
{
    // Getting the first one in the list
    var teamOne = await context.Teams.FirstAsync();
    if (teamOne != null) Console.WriteLine(teamOne.Name);

    // Select record that meets a condition or throws error
    var teamTwo = await context.Teams.FirstAsync(t => t.Id == 2);
    if (teamTwo != null) Console.WriteLine(teamTwo.Name);
// returns a record or null
    var teamTwoOrNull = await context.Teams.FirstOrDefaultAsync(t => t.Id == 2);
    if (teamTwoOrNull != null) Console.WriteLine(teamTwoOrNull.Name);
    //Selecting a single record - Only one record should be returned or returns error if there is more than one record that can be returned
    var teamThreeA = await context.Teams.SingleAsync(t => t.Id == 3);
    if (teamThreeA != null) Console.WriteLine(teamThreeA.Name);
    //Selecting a single record - Only one record should be returned or returns null
    var teamThreeB = await context.Teams.SingleOrDefaultAsync(t => t.Id == 3);
    if (teamThreeB != null) Console.WriteLine(teamThreeB);
// Selecting based on Id
    var teamBasedOnId = await context.Teams.FindAsync(5);
    if (teamBasedOnId != null) Console.WriteLine(teamBasedOnId.Name);
}