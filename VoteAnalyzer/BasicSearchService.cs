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
    public class BasicSearchService
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

        public BasicSearchService()
        {
        }

        public async Task<Candidate> FindCandidateInformation(string candidateNumber, string candidateRole)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;

            if(candidateRole.Equals("Deputy")){
                response = await httpClient.GetAsync(GET_DEPUTY_CANDIDATE);
                role = "Deputy";
            } else if (candidateRole.Equals("Senator")){
                response = await httpClient.GetAsync(GET_SENATOR_CANDIDATE);
                role = "Senator";
            } else {
                response = await httpClient.GetAsync(GET_PRESIDENT_CANDIDATE);
                role = "President";
            }


            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                var candidates = new List<Candidate>();

                JArray jsonResponse = JArray.Parse(content);
                Candidate candidate = null;

                foreach (JObject item in jsonResponse)
                {
                    string number = (string)item.GetValue("NUMBER");
                    if(number.Equals(candidateNumber)){
                        candidate = new Candidate();
                        candidate.Name = (string)item.GetValue("NAME");
                        candidate.Role = role;
                        candidate.Number = (string)item.GetValue("NUMBER");

                        if(role.Equals("President")){
                            candidate.Party = (string)item.GetValue("PARTY");
                        }
                    }


                }
                return candidate;

            }
            else
            {
                Console.Out.WriteLine("Try again. We couldn't get the data");
                return null;
            }
        }

        public async Task<Candidate> FindCandidateFullInfo(string candidateNumber, string candidateRole){
            Candidate candidate = await FindCandidateInformation(candidateNumber, candidateRole);
            Candidate refinedCandidate = null;
            rating = 0;

            if (candidate.Role.Equals("Deputy"))
            {
                refinedCandidate = await FindDeputyVotes(candidate);
                refinedCandidate = await FindCorruptionCases(refinedCandidate);
                role = "Deputy";
            }
            else if (candidate.Role.Equals("Senator"))
            {
                refinedCandidate = await FindSenatorVotes(candidate);
                refinedCandidate = await FindCorruptionCases(refinedCandidate);
                role = "Senator";
            }
            else
            {
                refinedCandidate = await FindCorruptionCases(candidate);
                refinedCandidate.CandidateVotes.Add("");
                refinedCandidate.CandidateVotes.Add("");
                role = "President";
            }

            return refinedCandidate;

        }

        public async Task<Candidate> FindDeputyVotes(Candidate candidate){
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_TEMER_DENOUNCE);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                JArray jsonResponse = JArray.Parse(content);

                foreach (JObject item in jsonResponse)
                {
                    string name = (string)item.GetValue("Name");
                    if (name.Equals(candidate.Name))
                    {
                        candidate.Party = (string)item.GetValue("Party");
                        string firstVote = "- In first denounce, voted " + (string)item.GetValue("First Denouncement");
                        candidate.CandidateVotes.Add(firstVote);

                        if (firstVote.Equals("AGAINST TEMER")){
                            rating += 40;
                        } else {
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
                    }


                }
                return candidate;

            }
            else
            {
                Console.Out.WriteLine("Try again. We couldn't get the data");
                return null;
            }
        }



        public async Task<Candidate> FindSenatorVotes(Candidate candidate)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_AECIO_VOTING);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                JArray jsonResponse = JArray.Parse(content);

                foreach (JObject item in jsonResponse)
                {
                    string name = (string)item.GetValue("Name");
                    if (name.Equals(candidate.Name))
                    {
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
                    }


                }

            }

            response = await httpClient.GetAsync(GET_WORK_REFORM);
                if (response != null || response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Console.Out.WriteLine("Response Body: \r\n {0}", content);

                    JArray jsonResponse = JArray.Parse(content);

                    foreach (JObject item in jsonResponse)
                    {
                        string name = (string)item.GetValue("Name");
                        if (name.Equals(candidate.Name))
                        {
                            candidate.Party = (string)item.GetValue("Party");

                            string firstVote = "- Voted " + (string)item.GetValue("Vote") + " the workers reform";
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
                return candidate;

            }
            else
            {
                Console.Out.WriteLine("Try again. We couldn't get the data");
                return null;
            }

        }

        /*
        public Candidate FindPresidentData(Candidate candidate)
        {

        }
        */

        public async Task<Candidate> FindCorruptionCases(Candidate candidate){
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await httpClient.GetAsync(GET_JANOT_LIST);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                JArray jsonResponse = JArray.Parse(content);

                foreach (JObject item in jsonResponse)
                {
                    string name = (string)item.GetValue("NAME");
                    if (name.Equals(candidate.Name))
                    {
                        string caughtInJanotList = "\n- Caught in Janots list while was " + (string)item.GetValue("ROLE");
                        candidate.CorruptionCases[0] += caughtInJanotList;
                        rating -= 40;
                        candidate.Rating = rating;
                    }

                }
            }

            response = await httpClient.GetAsync(GET_FACHIN_LIST);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                JArray jsonResponse = JArray.Parse(content);

                foreach (JObject item in jsonResponse)
                {
                    string name = (string)item.GetValue("NAME");
                    if (name.Equals(candidate.Name))
                    {
                        string caughtInFachinList = "\n- Caught in Fachins list while was " + (string)item.GetValue("ROLE");
                        candidate.CorruptionCases[0] += caughtInFachinList;
                        rating -= 40;
                        candidate.Rating = rating;
                    }

                }
            }

            response = await httpClient.GetAsync(GET_CARWASH_INVOLVMENT);
            if (response != null || response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.Out.WriteLine("Response Body: \r\n {0}", content);

                JArray jsonResponse = JArray.Parse(content);

                foreach (JObject item in jsonResponse)
                {
                    string name = (string)item.GetValue("NAME");
                    if (name.Equals(candidate.Name))
                    {
                        string caughtInCarwash = "\n - " +(string)item.GetValue("RESULT") + " at Carwash";
                        candidate.CorruptionCases[0] += caughtInCarwash;
                        rating -= 40;
                        candidate.Rating = rating;
                    }

                }
            }

            return candidate;
        }
    }
}
