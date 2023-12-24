using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

// Create instance of context
using var context = new FootballLeagueDbContext();
Console.WriteLine(context.DbPath);

//await GetAllTeams();
//await GetOneTeam();
//await GetFilteredTeams();
//await GetAggregateMethods();
//await GetGroupedMethod();
//await GetOrderedMethods();
//await GetPagination();
await GetSelectAndProjections();

async Task GetSelectAndProjections()
{
    var teams = await context.Teams.Select(x => x.Name).ToListAsync();

    foreach (var teamName in teams) Console.WriteLine(teamName);

    //Projection
    var teams2 = await context.Teams.Select(x => new { x.Name, x.CreatedDate }).ToListAsync();

    foreach (var team in teams2)
    {
        var creationTime = team.CreatedDate.TimeOfDay.ToString().Split('.');
        Console.WriteLine($"Team {team.Name} was created on {team.CreatedDate.Date} at {creationTime[0]}.");
    }
}

async Task GetPagination()
{
    var recordCount = 3;
    var page = 0;

    var next = true;
    while (next)
    {
        var teams = await context.Teams.Skip(page * recordCount).Take(recordCount).ToListAsync();

        foreach (var team in teams) Console.WriteLine(team.Name);
        ;
        Console.WriteLine("Enter 'true' for the next set of records, 'false' to exit");
        next = Convert.ToBoolean(Console.ReadLine());

        if (!next) break;
        page += 1;
    }
}

async Task GetOrderedMethods()
{
    var orderedTeamsAscending = await context.Teams.OrderBy(t => t.Name).ToListAsync();
    foreach (var team in orderedTeamsAscending) Console.WriteLine(team.Name);

    var orderedTeamsDescending = await context.Teams.OrderByDescending(t => t.Name).ToListAsync();
    foreach (var team in orderedTeamsDescending) Console.WriteLine(team.Name);

    // Get record with maximum value
    var maxBy = context.Teams.MaxBy(q => q.Id);
    Console.WriteLine(maxBy);

    // Get record with minimum value
    var minBy = context.Teams.MinBy(b => b.Id);
    Console.WriteLine(minBy);
}

async Task GetGroupedMethod()
{
    var groupedTeams = await context.Teams.GroupBy(t => new { t.CreatedDate.Date, t.Name }).ToListAsync();

    foreach (var group in groupedTeams)
    {
        Console.WriteLine(group.Key);
        foreach (var team in group) Console.WriteLine(team.Name);
    }
}


async Task GetAggregateMethods()
{
    // Count
    var numberOfTeams = await context.Teams.CountAsync();
    Console.WriteLine(numberOfTeams);

    var numberOfTeamsFiltered = await context.Teams.CountAsync(q => q.Name.Contains("Huskers"));
    Console.WriteLine(numberOfTeamsFiltered);
    // Max
    var maxTeamId = await context.Teams.MaxAsync(t => t.Id);
    Console.WriteLine(maxTeamId);
    // Min
    var minTeamId = await context.Teams.MinAsync(t => t.Id);
    Console.WriteLine(minTeamId);
    // Average
    var averageTeamId = await context.Teams.AverageAsync(t => t.Id);
    Console.WriteLine(averageTeamId);
    // Sum
    var sumTeamId = await context.Teams.SumAsync(t => t.Id);
    Console.WriteLine(sumTeamId);
}

async Task GetFilteredTeams()
{
    Console.WriteLine("Enter Desired Team");
    var desiredTeam = Console.ReadLine();
    //Select all records that meet a condition
    var teamsFiltered = await context.Teams.Where(q => q.Name == desiredTeam).ToListAsync();
    foreach (var team in teamsFiltered) Console.WriteLine(team.Name);

    Console.WriteLine("Enter Search Term");
    var searchTerm = Console.ReadLine();
    var partialMatches = await context.Teams.Where(q => q.Name.Contains(searchTerm)).ToListAsync();
    foreach (var match in partialMatches) Console.WriteLine(match.Name);
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