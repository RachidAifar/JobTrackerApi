import { useEffect, useState } from "react";
import api from "../services/api";

function Dashboard() {
  const [jobs, setJobs] = useState([]);

  useEffect(() => {
    const fetchJobs = async () => {
      try {
        const token = localStorage.getItem("token");

        if (!token) {
            console.log("No token found");
            return;
        }

        

        const response = await api.get("/Jobs/myjobs", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        console.log(response.data);

        setJobs(response.data);
      } catch (error) {
        console.error("Error fetching jobs:", error);
      }
    };

    fetchJobs();
  }, []);

  return (
    <div>
      <h1>My Jobs</h1>

      {jobs.length === 0 ? (
        <p>No jobs found.</p>
      ) : (
        jobs.map((job) => (
          <div key={job.id}>
            <h3>{job.company}</h3>
            <p>{job.position}</p>
            <p>{job.status}</p>
          </div>
        ))
      )}
    </div>
  );
}

export default Dashboard;