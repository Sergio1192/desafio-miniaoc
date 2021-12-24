string[] input = new[] {
    "hola@somoshackersastutos.com",
    "ambrosio@outlook.com",
    "coco@malandriners.dev",
    "hello@somoshackersastutos.com",
    "ambrosio@outlook.com",
    "ciao@somoshackersastutos.com"
};

HackerDetector hackerDetector = new();
var hackers = hackerDetector.FindSmartHackers(input);

hackers.ToList().ForEach(element => Console.WriteLine(element));

public record EmailDomain(string Email, string Domain);

class HackerDetector
{
    public IEnumerable<string> FindSmartHackers(string[] emails)
    {
        var emailsAux = GetEmailsWithDomain(emails);
        var hackerDomain = GetHackerDomain(emailsAux);
        var hackerEmails = GetHackerEmails(emailsAux, hackerDomain);

        return hackerEmails;
    }

    private IEnumerable<EmailDomain> GetEmailsWithDomain(string[] emails) {
        return emails.Select(email => new EmailDomain(email, email.Split('@').Last()));
    }

    private string GetHackerDomain(IEnumerable<EmailDomain> emails) {  
        var domainsGroup = emails.GroupBy(
            email => email.Domain,
            (domain, domains) => new
            {
                Domain = domain,
                Count = domains.Count(),
            }
        );

        var hackerDomain = domainsGroup.MaxBy(domainG => domainG.Count);

        return hackerDomain?.Domain ?? string.Empty;
    }

    public IEnumerable<string> GetHackerEmails(IEnumerable<EmailDomain> emails, string hackerDomain)
    {
        var hackerEmails = emails.Where(email => email.Domain.Equals(hackerDomain));

        return hackerEmails.Select(email => email.Email).ToArray();
    }
}
