FROM mcr.microsoft.com/devcontainers/javascript-node:1-20-bullseye

# dotnet インストール
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    sudo dpkg -i packages-microsoft-prod.deb \
    rm packages-microsoft-prod.deb \
    apt-get update \
    && apt-get install -y --no-install-recommends \
    dotnet-sdk-9.0 \
    aspnetcore-runtime-9.0 \
    && apt-get -y clean \
    && rm -rf /var/lib/apt/lists/*

# Gemini cli インストール
RUN npm install -g npm@11.4.2 @google/gemini-cli

USER node