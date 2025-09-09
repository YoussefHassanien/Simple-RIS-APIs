// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
  loadPatients();
});

function loadPatients() {
  // Show loading indicator
  $("#loadingIndicator").removeClass("d-none");
  $("#errorAlert").addClass("d-none");
  $("#statsRow").addClass("d-none");
  $("#tableContainer").addClass("d-none");
  $("#noDataMessage").addClass("d-none");

  // Enhanced AJAX call with comprehensive error handling
  $.ajax({
    url: "https://localhost:7099/api/Patients/all/details",
    type: "GET",
    dataType: "json",
    timeout: 30 * 1000, // 30 second timeout
    beforeSend: function (xhr) {
      xhr.setRequestHeader("Accept", "application/json");
    },
    success: function (data, textStatus, xhr) {
      $("#loadingIndicator").addClass("d-none");

      // Validate and process the response
      if (data && Array.isArray(data) && data.length > 0) {
        try {
          populateTable(data);
          updateStatistics(data);
          $("#statsRow").removeClass("d-none");
          $("#tableContainer").removeClass("d-none");
        } catch (processingError) {
          $("#errorMessage").text(
            "Error processing patient data: " + processingError.message
          );
          $("#errorAlert").removeClass("d-none");
          $("#noDataMessage").removeClass("d-none");
        }
      } else {
        $("#noDataMessage").removeClass("d-none");
      }
    },
    error: function (xhr, status, error) {
      $("#loadingIndicator").addClass("d-none");

      let errorMessage = "Error loading patient data: ";

      switch (xhr.status) {
        case 0:
          errorMessage +=
            "Unable to connect to API. Please check if the RIS API is running.";
          break;
        case 404:
          errorMessage += "API endpoint not found. Please verify the API URL.";
          break;
        case 500:
          errorMessage += "Internal server error. Please try again later.";
          break;
        case 503:
          errorMessage += "Service unavailable. Please try again later.";
          break;
        default:
          errorMessage += `${xhr.status} - ${xhr.statusText || error}`;
      }

      if (status === "timeout") {
        errorMessage =
          "Request timed out. Please check your connection and try again.";
      } else if (status === "parsererror") {
        errorMessage = "Error parsing server response. Please try again.";
      }

      $("#errorMessage").text(errorMessage);
      $("#errorAlert").removeClass("d-none");
      $("#noDataMessage").removeClass("d-none");
    },
  });
}

function populateTable(patients) {
  var tbody = $("#patientsTableBody");
  tbody.empty();

  patients.forEach(function (patient, index) {
    var row = $(`
                    <tr data-patient-index="${index}">
                        <td><span class="badge bg-secondary">${
                          patient.personId
                        }</span></td>
                        <td>
                            <div class="d-flex align-items-center">
                                <strong>${patient.patientName}</strong>
                            </div>
                        </td>
                        <td><a class="text-decoration-none">${
                          patient.mobileNumber
                        }</a></td>
                        <td>${patient.dateOfBirth}</td>
                        <td><span class="badge ${
                          patient.gender.toLowerCase() === "m"
                            ? "bg-info"
                            : "bg-pink"
                        }">${
      patient.gender.toLowerCase() === "m" ? "male" : "female"
    }</span></td>
                        <td><code>${patient.socialSecurityNumber}</code></td>
                        <td><span class="badge bg-info">${
                          patient.patientId
                        }</span></td>
                        <td><span class="badge ${
                          patient.isVip
                            ? "bg-warning text-dark"
                            : "bg-secondary"
                        }">${patient.isVip ? "VIP" : "Regular"}</span></td>
                        <td><span class="badge ${
                          patient.isActive ? "bg-success" : "bg-danger"
                        }">${
      patient.isActive ? "Active" : "Inactive"
    }</span></td>
                        <td>${
                          patient.studyId
                            ? '<span class="badge bg-primary">' +
                              patient.studyId +
                              "</span>"
                            : '<span class="text-muted">-</span>'
                        }</td>
                        <td style="min-width: 200px;">${formatDateTime(
                          patient.studyCreatedAt
                        )}</td>
                        <td>${formatDateTime(patient.studyUpdatedAt)}</td>
                        <td>${
                          patient.serviceId
                            ? '<span class="badge bg-success">' +
                              patient.serviceId +
                              "</span>"
                            : '<span class="text-muted">-</span>'
                        }</td>
                        <td>${
                          patient.serviceType ||
                          '<span class="text-muted">-</span>'
                        }</td>
                        <td>${
                          patient.serviceDescription ||
                          '<span class="text-muted">-</span>'
                        }</td>
                        <td>${
                          patient.serviceCost
                            ? "<span>" +
                              patient.serviceCost +
                              " " +
                              patient.serviceCurrency +
                              "</span>"
                            : '<span class="text-muted">-</span>'
                        }</td>
                        <td>${
                          patient.doctorId
                            ? '<span class="badge bg-primary">' +
                              patient.doctorId +
                              "</span>"
                            : '<span class="text-muted">-</span>'
                        }</td>
                        <td>${
                          patient.doctorName ||
                          '<span class="text-muted">-</span>'
                        }</td>
                    </tr>
                `);

    // Store original patient data for serialization
    row.data("patient", patient);
    tbody.append(row);
  });

  // Store patients data globally for serialization
  window.currentPatientsData = patients;
}

function updateStatistics(patients) {
  const total = patients.length;
  const active = patients.filter((p) => p.isActive).length;
  const vip = patients.filter((p) => p.isVip).length;

  $("#totalCount").text(`Total Patients: ${total}`);
  $("#activeCount").text(`Active: ${active}`);
  $("#vipCount").text(`VIP: ${vip}`);
}

// Helper function to format date/time to ISO string with better display
function formatDateTime(dateString) {
  if (!dateString) return '<span class="text-muted">-</span>';
  try {
    const date = new Date(dateString);
    // Convert to ISO string and format for display
    const isoString = date.toISOString();
    // Display local format without seconds
    const localString = date.toLocaleString(undefined, {
      year: "numeric",
      month: "2-digit",
      day: "2-digit",
      hour: "2-digit",
      minute: "2-digit",
    });
    return `<span title="${isoString}">${localString}</span>`;
  } catch (error) {
    console.warn("Invalid date format:", dateString);
    return '<span class="text-muted">Invalid Date</span>';
  }
}
