# Hasta Takip Uygulaması

## Description
The Hasta Takip Uygulaması (Patient Follow-Up Application) is a system developed to monitor patient health metrics using wireless communication with Xbee and Arduino. This application collects data from various sensors and transmits it wirelessly to a web interface for real-time monitoring. The data is collected using C# and stored in a local database. The project was completed by a team of three members.

## Features
- **Wireless Communication**: Utilizes Xbee technology to establish connections between sensors and the central monitoring system.
- **Sensors Used**:
  - **DHT11**: Measures temperature and humidity.
  - **Pulse Sensor**: Monitors heart rate.
  - **LM35**: Measures body temperature.
- **Data Handling**: Sensor data is received using C# and stored in a local database for further analysis.
- **Web Interface**: A user-friendly web interface developed using C# for real-time data visualization and monitoring.

## Toolbox

### Languages
- C#
- C++
- PHP
- JavaScript
- HTML
- CSS3

### Frameworks and Libraries
- .NET Framework
- MVC
- Bootstrap
- jQuery
- Ajax

### Database
- MSSQL

### Hardware
- Arduino
- Xbee

### Sensors
- **DHT11**: Temperature and Humidity Sensor
- **Pulse Sensor**: Heart Rate Monitor
- **LM35**: Temperature Sensor

## Installation

### Prerequisites
- Arduino IDE
- Visual Studio (or any C# IDE)
- MSSQL Server
- Xbee modules

### Steps

1. **Set Up Arduino**:
   - Connect the DHT11, Pulse Sensor, and LM35 sensors to the Arduino.
   - Upload the Arduino code to read sensor data.

2. **Configure Xbee Modules**:
   - Set up Xbee modules for wireless communication between Arduino and the central system.

3. **Database Setup**:
   - Set up an MSSQL database to store the sensor data.
   - Create necessary tables for storing temperature, humidity, and heart rate data.

4. **Develop the Web Interface**:
   - Use Visual Studio to create a C# project.
   - Develop the web interface using HTML, CSS3, JavaScript, Bootstrap, jQuery, and Ajax.
   - Implement server-side logic using the .NET Framework and MVC.

5. **Data Collection and Transmission**:
   - Write C# code to receive data from the Arduino via Xbee.
   - Store the received data in the MSSQL database.

6. **Real-Time Monitoring**:
   - Implement real-time data visualization on the web interface.
   - Ensure the web interface is user-friendly and provides real-time updates.

## Usage
- Start the Arduino to begin collecting sensor data.
- The data is transmitted wirelessly via Xbee to the central monitoring system.
- Use the web interface to monitor the real-time health metrics of patients.
- Access stored data in the MSSQL database for analysis and reporting.

## Contributors
- **Yunus Koç**: Hardware integration and sensor setup
- **Tahsin Soyak & Yunus Koç**: Software development and database management
- **Emirhan Dikçi & Tahsin Soyak**: Web interface design and implementation

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements
- Thanks to all the team members for their contributions and hard work.
- Special thanks to the open-source community for providing valuable resources and support.
