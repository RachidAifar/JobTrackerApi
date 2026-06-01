import { useEffect, useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

function Dashboard() {
  const [jobs, setJobs] = useState([]);
  const navigate = useNavigate();
  const handleLogout = () => {
  localStorage.removeItem("token");
  navigate("/");
    };
  useEffect(() => {
    const fetchJobs = async () => {
      try {
        const token = localStorage.getItem("token");

        if (!token) {
            navigate("/");
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
  },  [navigate]);

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
        <button onClick={handleLogout}>
            Logout
        </button>
    </div>
  );
}

export default Dashboard;