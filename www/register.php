<?php
	// Connect to database
    if(isset($_POST["Login"]) && isset($_POST["password1"]) && isset($_POST["password2"]) && isset($_POST["LastName"]) && isset($_POST["Name"]) && isset($_POST["Position"]) && isset($_POST["Phone"]) && isset($_POST["Squad_Id"]))
	{
		$errors = array();
		
		$loginMaxLength = 20;
		$loginMinLength = 3;
		$passwordMaxLength = 19;
		$passwordMinLength = 5;
		$lastNameMaxLength = 50;
		$lastNameMinLength = 3;
		$nameMaxLength = 50;
		$nameMinLength = 3;
		$phoneLength = 12;
		
		$login = $_POST["Login"];
		$password1 = $_POST["password1"];
		$password2 = $_POST["password2"];
		$lastname = $_POST["LastName"];
		$name = $_POST["Name"];
		$position = $_POST["Position"];
		$phone = $_POST["Phone"];
		$squadId = $_POST["Squad_Id"];
		
		//Validate login
		if(preg_match('/\s/', $login)){
			$errors[] = "Логин не может иметь пробелов";
		}else{
			if(strlen($login) > $loginMaxLength || strlen($login) < $loginMinLength){
				$errors[] = "Длина логина должна быть от " . strval($loginMinLength) . " до " . strval($loginMaxLength) . " знаков";
			}else{
				if(!ctype_alnum ($login)){
					$errors[] = "Логин должен содержать только цифры и латинские буквы";
				}
			}
		}
		
		//Validate password
		if($password1 != $password2){
			$errors[] = "Пароли не совпадают";
		}else{
			if(preg_match('/\s/', $password1)){
				$errors[] = "Пароль не может иметь пробелов";
			}else{
				if(strlen($password1) > $passwordMaxLength || strlen($password1) < $passwordMinLength){
					$errors[] = "Длина пароля должна быть от " . strval($passwordMinLength) . " до " . strval($passwordMaxLength) . " знаков";
				}else{
					if(!preg_match('/[A-Za-z]/', $password1) || !preg_match('/[0-9]/', $password1)){
						$errors[] = "Пароль должен содержать и цифры и латинские буквы";
					}
				}
			}
		}

		//Validate last name
		if(preg_match('/\s/', $lastname)){
			$errors[] = "Фамилия не может иметь пробелов";
		}else{
			if(!validate_lastname($lastname)){
				$errors[] = "Фамилия должна начинаться с заглавной буквы и быть на кириллице";
			}else{
				if(strlen($lastname) > $lastNameMaxLength || strlen($lastname) < $lastNameMinLength){
					$errors[] = "Длина фамилии должна быть от " . strval($lastNameMinLength) . " до " . strval($lastNameMaxLength) . " знаков";
				}
			}
		}

		//Validate name
		if(preg_match('/\s/', $name)){
			$errors[] = "Имя не может иметь пробелов";
		}else{
			if(!validate_name($name)){
				$errors[] = "Имя должно начинаться с заглавной буквы и быть на кириллице";
			}else{
				if(strlen($name) > $nameMaxLength || strlen($name) < $nameMinLength){
					$errors[] = "Длина имени должна быть от" . strval($nameMinLength) . " до " . strval($nameMaxLength) . " знаков";
				}
			}
		}

		//Validate phone
		if(preg_match('/\s/', $phone)){
			$errors[] = "Номер телефона не может иметь пробелов";
		}else{
			if(!validate_phone($phone)){
				$errors[] = "Некорректный ввод номера телефона";
			}
		}

		//Check if there is user already registered with the same login
		if(count($errors) == 0){
			//Connect to database
			require dirname(__FILE__) . '/database.php';
			
			if ($stmt = $mysqli_conection->prepare("SELECT Login FROM Users WHERE Login = ? LIMIT 1")) {
				
				/* bind parameters for markers */
				$stmt->bind_param('s', $login);
					
				/* execute query */
				if($stmt->execute()){
					
					/* store result */
					$stmt->store_result();

					if($stmt->num_rows > 0){
					
						/* bind result variables */
						$stmt->bind_result($login_tmp);

						/* fetch value */
						$stmt->fetch();
						
						if($login_tmp == $login){
							$errors[] = "Пользователь с таким логином уже существует";
						}
					}
					
					/* close statement */
					$stmt->close();
					
				}else{
					$errors[] = "Что-то пошло не так, попробуй снова. Ошибка #01";
				}
			}else{
				$errors[] = "Что-то пошло не так, попробуй снова. Ошибка #02";
			}
		}

		if($position == "Командир")
		$positionToCheck = "Commander";
		else if($position == "Комиссар")
			$positionToCheck = "Commissioner";
		else if($position == "Пресс-секретарь")
			$positionToCheck = "Press";
		else if($position == "Бригадир")
			$positionToCheck = "";
		else if($position == "Методист")
			$positionToCheck = "";
		else if($position == "Мастер")
			$positionToCheck = "Master";
		else if($position == "Боец")
			$positionToCheck = "";
		else if($position == "Кандидат")
			$positionToCheck = "";

		if ($positionToCheck != "")
		//Check if there is user with the same position
		if(count($errors) == 0){
			//Connect to database
			require dirname(__FILE__) . '/database.php';
			
			if ($stmt = $mysqli_conection->prepare("SELECT ".$positionToCheck." FROM Squads WHERE Squad_Id = ? LIMIT 1")) {
				
				/* bind parameters for markers */
				$stmt->bind_param('s', $squadId);
					
				/* execute query */
				if($stmt->execute()){
					
					/* store result */
					$stmt->store_result();

					if($stmt->num_rows > 0){
					
						$errors[] = "Пользователь с такой должностью уже существует";
					}
					
					/* close statement */
					$stmt->close();
					
				}else{
					$errors[] = "Что-то пошло не так, попробуй снова. Ошибка #05";
				}
			}else{
				$errors[] = "Что-то пошло не так, попробуй снова. Ошибка #06";
			}
		}
		
		//Finalize registration
		if(count($errors) == 0){
			$hashedPassword = password_hash($password1, PASSWORD_BCRYPT);
			if ($stmt = $mysqli_conection->prepare("INSERT INTO Users (User_Id, Login, Password, LastName, Name, Phone, Squad_Id, Squad_Id_OUD, Position) VALUES('', ?, ?, ?, ?, ?, ?, '', ?)")) {
				
				/* bind parameters for markers */
				$stmt->bind_param('sssssss', $login, $hashedPassword, mb_convert_encoding($lastname, 'windows-1251', 'utf-8'), 
				mb_convert_encoding($name, 'windows-1251', 'utf-8'), $phone, $squadId, mb_convert_encoding($position, 'windows-1251', 'utf-8'));
					
				/* execute query */
				if($stmt->execute()){
					
					/* close statement */
					$stmt->close();
					
				}else{
					$errors[] = "Что-то пошло не так, попробуй снова. Ошибка #03";
				}
			}else{
				$errors[] = "Что-то пошло не так, попробуй снова. Ошибка #04";
			}
		}
		
		if(count($errors) > 0){
			echo $errors[0];
		}else{
			echo "Success";
		}
	}else{
		echo "Missing data";
	}
	
	function validate_lastname($lastname) {
		return preg_match('/^[А-Я][а-я]+$/u', $lastname);
	}
	function validate_name($name) {
		return preg_match('/^[А-Я][а-я]+$/u', $name);
	}
	function validate_phone($phone) {
		return preg_match('/^[8][\d]{10}$/', $phone);
	}
?>