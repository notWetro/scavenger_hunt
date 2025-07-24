# React Scavenger Hunt Application

This project is a React-based application for creating and managing scavenger hunts. 

## Project Structure

```
frontend
├── src
│   ├── pages
│   │   └── EditHunt.jsx
│   └── index.js
├── public
│   └── index.html
├── package.json
├── Dockerfile
└── README.md
```

## Getting Started

To get a copy of this project up and running on your local machine, follow these steps:

### Prerequisites

- Node.js (version 14 or higher)
- npm (Node package manager)

### Installation

1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd frontend
   ```
3. Install the dependencies:
   ```
   npm install
   ```

### Running the Application

To start the development server, run:
```
npm start
```
This will start the application on `http://localhost:3000`.

### Building for Production

To create a production build of the application, run:
```
npm run build
```
This will generate a `build` directory with the optimized production files.

### Docker

To build and run the application using Docker, follow these steps:

1. Build the Docker image:
   ```
   docker build -t scavenger-hunt .
   ```
2. Run the Docker container:
   ```
   docker run -p 3000:3000 scavenger-hunt
   ```

### Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request with your changes.

### License

This project is licensed under the MIT License. See the LICENSE file for details.