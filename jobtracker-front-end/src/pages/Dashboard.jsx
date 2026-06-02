import { useEffect, useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

function Dashboard() {
  const [jobs, setJobs] = useState([]);
  const navigate = useNavigate();

  const [company, setCompany] = useState("");
  const [position, setPosition] = useState("");

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  // ✅ FETCH JOBS
  const fetchJobs = async () => {
    try {
      const token = localStorage.getItem("token");

      if (!token) {
        navigate("/");
        return;
      }

      const response = await api.get("/jobs/myjobs", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });

      setJobs(response.data);
    } catch (error) {
      console.error("Error fetching jobs:", error);
    }
  };

  // ✅ ADD JOB (MUST BE OUTSIDE useEffect)
  const handleAddJob = async () => {
    try {
      const token = localStorage.getItem("token");

      await api.post(
        "/Jobs",
        { company, position },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

      setCompany("");
      setPosition("");

      fetchJobs(); // refresh list
    } catch (error) {
      console.error("Error adding job:", error);
    }
  };
  const handleDeleteJob = async (jobId) => {
  try {
    const token = localStorage.getItem("token");

    await api.delete(`/Jobs/${jobId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    fetchJobs();
  } catch (error) {
    console.error("Error deleting job:", error);
  }
};

  useEffect(() => {
    fetchJobs();
  }, [navigate]);

  return (
    <div>
      <div style={{ marginBottom: "20px" }}>
        <h2>Add Job</h2>

        <input
          placeholder="Company"
          value={company}
          onChange={(e) => setCompany(e.target.value)}
        />

        <br /><br />

        <input
          placeholder="Position"
          value={position}
          onChange={(e) => setPosition(e.target.value)}
        />

        <br /><br />

        <button onClick={handleAddJob}>
          Add Job
        </button>
      </div>

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
      <button onClick={() => handleDeleteJob(job.id)}>
         Delete
      </button>
    </div>
  );
}

export default Dashboard;