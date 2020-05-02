using System;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace VoteAnalyzer
{
    public class RankingService
    {
        private const string GET_DEPUTY_CANDIDATE = "https://api.myjson.com/bins/b7do1";
        private const string GET_SENATOR_CANDIDATE = "https://api.myjson.com/bins/19qir5";
        private const string GET_PRESIDENT_CANDIDATE = "https://api.myjson.com/bins/cge1d";
        private string role;
        private int rating;

        //For Deputies
        private const string GET_TEMER_DENOUNCE = "https://api.myjson.com/bins/19wqip";

        //For Senators
        private const string GET_AECIO_VOTING = "https://api.myjson.com/bins/1fbs4h";
        private const string GET_WORK_REFORM = "https://api.myjson.com/bins/1djhb5";

        //General info
        private const string GET_JANOT_LIST = "https://api.myjson.com/bins/1ai64h";
        private const string GET_FACHIN_LIST = "https://api.myjson.com/bins/hzi8x";
        private const string GET_CARWASH_INVOLVMENT = "https://api.myjson.com/bins/1ccm3l";

        public RankingService()
        {
        }

        public async Task<List<Candidate>> FindDeputyRanking()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_TEMER_DENOUNCE);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);
                JArray jsonResponse = JArray.Parse(content);

                List<Candidate> candidates = new List<Candidate>();
                Candidate candidate;
                foreach (JObject item in jsonResponse)
                {
                        rating = 0;
                        candidate = new Candidate();
                        candidate.Name = (string)item.GetValue("Name");
                        candidate.Party = (string)item.GetValue("Party");
                        string firstVote = "- In first denounce, voted " + (string)item.GetValue("First Denouncement");
                        candidate.CandidateVotes.Add(firstVote);

                        if (firstVote.Equals("AGAINST TEMER"))
                        {
                            rating += 40;
                        }
                        else
                        {
                            rating += 20;
                        }

                        string secondVote = "- In second denounce, voted " + (string)item.GetValue("Second Denouncement");
                        candidate.CandidateVotes.Add(secondVote);

                        if (secondVote.Equals("AGAINST TEMER"))
                        {
                            rating += 40;
                        }
                        else
                        {
                            rating += 20;
                        }

                        candidate.Rating = rating;


                    candidates.Add(candidate);

                }

                for (int i = 0; i < candidates.Count; i++){
                    for (int j = 0; j < i - 1; j++)
                    {
                        if (candidates[j].Rating < candidates[j+1].Rating)
                        {
                            Candidate temporary = candidates[j];
                            candidates[j] = candidates[j+1];
                            candidates[j+1] = temporary;
                        }
                    }
                }

                return candidates;
            }
            else
            {
                Console.Out.WriteLine("Try again. We couldn't get the data");
                return null;
            }
        }


        public async Task<List<Candidate>> FindSenatorRanking()
        {
         HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_AECIO_VOTING);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);
                JArray jsonResponse = JArray.Parse(content);

                List<Candidate> candidates = new List<Candidate>();
                Candidate candidate;
                foreach (JObject item in jsonResponse)
                {
                        rating = 0;
                        candidate = new Candidate();
                        candidate.Name = (string)item.GetValue("Name");
                        candidate.Party = (string)item.GetValue("Party");

                        string firstVote = "- Voted " + (string)item.GetValue("Vote");
                        candidate.CandidateVotes.Add(firstVote);

                        if (firstVote.Equals("AGAINST AECIO"))
                        {
                            rating += 40;
                        }
                        else
                        {
                            rating += 20;
                        }

                        candidate.Rating = rating;

                    response = await httpClient.GetAsync(GET_WORK_REFORM);
                    if (response != null || response.IsSuccessStatusCode)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);

                        jsonResponse = JArray.Parse(content);

                        foreach (JObject itemJson in jsonResponse)
                        {
                            string name = (string)itemJson.GetValue("Name");
                            if (name.Equals(candidate.Name))
                            {
                                candidate.Party = (string)itemJson.GetValue("Party");

                                firstVote = "- Voted " + (string)itemJson.GetValue("Vote") + " the workers reform";
                                candidate.CandidateVotes.Add(firstVote);

                                if (firstVote.Equals("AGAINST"))
                                {
                                    rating += 40;
                                }
                                else
                                {
                                    rating += 20;
                                }

                                candidate.Rating = rating;
                            }


                        }

                    }
                    candidates.Add(candidate);

                }

                for (int i = 0; i < candidates.Count; i++){
                    for (int j = 0; j < i - 1; j++)
                    {
                        if (candidates[j].Rating < candidates[j+1].Rating)
                        {
                            Candidate temporary = candidates[j];
                            candidates[j] = candidates[j+1];
                            candidates[j+1] = temporary;
                        }
                    }
                }

                return candidates;
            }
            else
            {
                Console.Out.WriteLine("Try again. We couldn't get the data");
                return null;
            }
        }



    }
}
