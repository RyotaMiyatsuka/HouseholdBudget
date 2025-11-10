FROM mcr.microsoft.com/devcontainers/javascript-node:1-20-bullseye

# dotnet インストール
RUN wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
    dotnet-sdk-9.0 \
    aspnetcore-runtime-9.0 \
    && apt-get -y clean \
    && rm -rf /var/lib/apt/lists/*

USER node

# dotnet-ef インストール
RUN dotnet tool install --global dotnet-ef

# Angular CLI / Gemini CLI / prism インストール
# hadolint ignore=DL3016
RUN npm install -g \
        @angular/cli \
        npm@11.4.2 \
        @google/gemini-cli \
        @stoplight/prism-cli \
        @tailwindcss/postcss postcss

# uv インストール
RUN curl -LsSf https://astral.sh/uv/install.sh | sh
ENV PATH="/home/node/.local/bin:${PATH}"
WORKDIR /workspace
