# covid API Slovenia

.NET core 5.0 API for getting formatted regional information about number of Covid-19 infections and vaccinations. Uses data from https://covid-19.sledilnik.org/sl/data.

## Usage:
Data is keyed by date of entries in csv file used like YYYY-MM-DD, for example 2021-09-10.
- GET /api/region: gets all entries
- GET /api/region/<YYYY-MM-DD>: gets the entry for that date
- GET /api/region/cases?region=<RR>&&from=<YYYY-MM-DD>&&to=<YYYY-MM-DD>: region required, from and to are optional. Gets entries for specified region between the given dates.
- GET /api/region/lastweek: makes a sum of cases for each region in the last 7 days from the day of the request and displays them
