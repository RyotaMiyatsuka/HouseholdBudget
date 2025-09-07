FROM mcr.microsoft.com/devcontainers/javascript-node:1-20-bullseye


# dotnet インストール
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
    dotnet-sdk-9.0 \
    aspnetcore-runtime-9.0 \
    && apt-get -y clean \
    && rm -rf /var/lib/apt/lists/*

USER node
WORKDIR /workspace
