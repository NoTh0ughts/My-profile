# Use the official node image as the base image
FROM node:16

# Set the working directory to /app
WORKDIR /app

# Copy the package.json and package-lock.json files
COPY package.json .
COPY package-lock.json .
COPY .env .
COPY .env.development .

# Install the dependencies for the React app
RUN npm install

# Copy the rest of the React app source code
COPY public ./public
COPY src ./src

# Build the React app
RUN npm run build

# Expose port 3000 for the React app
EXPOSE 80
EXPOSE 44492

# Start the React app
CMD ["npm", "start"]