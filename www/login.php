<?php

if(isset($_POST["login"]) && isset($_POST["password"])){
    $errors = array();
    
    $login = $_POST["login"];
    $password = $_POST["password"];
    
    //Connect to database
    require dirname(__FILE__) . '/database.php';
    
    if ($stmt = $mysqli_conection->prepare("SELECT Login, Password FROM Users WHERE Login = ? LIMIT 1")) {
        
        /* bind parameters for markers */
        $stmt->bind_param('s', $login);
            
        /* execute query */
        if($stmt->execute()){
            
            /* store result */
            $stmt->store_result();

            if($stmt->num_rows > 0){
                /* bind result variables */
                $stmt->bind_result($login_tmp, $password_hash);

                /* fetch value */
                $stmt->fetch();
                
                if(password_verify ($password, $password_hash)){
                    echo "Success" . "|" .  $login_tmp;
                    
                    return;
                }else{
                    $errors[] = "Нет такой комбинации логина и пароля";
                }
            }else{
                $errors[] = "Нет такой комбинации логина и пароля";
            }
            
            /* close statement */
            $stmt->close();
            
        }else{
            $errors[] = "Что-то пошло не так, попробуй снова. Ошибка #05";
        }
    }else{
        $errors[] = "Что-то пошло не так, попробуй снова. Ошибка #06";
    }
    
    if(count($errors) > 0){
        echo $errors[0];
    }
}else{
    echo "Не хватает данных";
}

?>