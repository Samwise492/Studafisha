<?php
  // Connect to database
  require dirname(__FILE__) . '/database.php';
  
  $sql = "SELECT * FROM Shop"; // query
  $result = $mysqli_conection->query($sql);

  if ($result->num_rows > 0) {
    // output data of each row
    while($row = $result->fetch_assoc()) {
      echo $row["Id"]." ".$row["Link"]." ".$row["Price"]." ".$row["Scores_for_discount"]." ".$row["Discounted_price"].'|';
    }
  } else {
    echo "0 results";
  }
  $mysqli_conection->close();
  ?>